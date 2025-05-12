using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace CMMFileDatabase.FileDatabase;

public class FileDatabaseManager : IFileDatabaseManager
{
    private readonly string _fileExtension;
    private readonly string _directoryName;
    private readonly string? _directoryPath;
    private readonly JsonSerializerOptions _jsonOptions;

    public FileDatabaseManager(FileDBRepositorySettings settings)
    {
        _fileExtension = settings.FileExtension;
        _directoryName = settings.DirectoryName;
        _directoryPath = settings.DirectoryPath;
        _jsonOptions = new JsonSerializerOptions { WriteIndented = true };  // Pretty print
    }

    public void Upsert<T>(T model) where T : FileModelBase
    {
        List<T> existingModels = InternalLoad<T>();
        model.LastModified = DateTime.UtcNow;

        var oldModel = existingModels.FirstOrDefault(x => x.Id == model.Id);
        if (oldModel is null)
        {
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();

            existingModels.Add(model);
        }
        else
        {
            existingModels.Remove(oldModel);
            existingModels.Add(model);
        }

        InternalStore(existingModels);
    }

    public T? GetEntryById<T>(Guid id) where T : FileModelBase
    {
        List<T> existingModels = InternalLoad<T>();
        return existingModels.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<T> GetAllEntries<T>() where T : FileModelBase
    {
        return InternalLoad<T>();
    }

    public IEnumerable<T> GetByKeyStr<T>(string key, string value) where T : FileModelBase
    {

        List<T> results = new();
        PropertyInfo? matchingKey = typeof(T).GetProperties().FirstOrDefault(x => x.Name == key);

        if (matchingKey is null)
            return new List<T>();

        List<T> existingModels = InternalLoad<T>();
        foreach (var model in existingModels)
        {
            object? valueObj = matchingKey.GetValue(model);

            if (valueObj is null)
                continue;

            if (valueObj.ToString() != value)
                continue;

            results.Add(model);
        }

        return results;
    }

    public T? GetOldest<T>() where T : FileModelBase
    {
        List<T> existingModels = InternalLoad<T>();
        return existingModels.OrderBy(o => o.Created).FirstOrDefault();
    }

    public T? GetNewest<T>() where T : FileModelBase
    {
        List<T> existingModels = InternalLoad<T>();
        return existingModels.OrderByDescending(o => o.Created).FirstOrDefault();
    }

    public void DeleteEntry<T>(Guid id) where T : FileModelBase
    {
        List<T> existingModels = InternalLoad<T>();
        var oldModel = existingModels.FirstOrDefault(x => x.Id == id);

        if (oldModel is null)
            return;

        existingModels.Remove(oldModel);
        InternalStore(existingModels);
    }

    public void DeleteEntry<T>(T model) where T : FileModelBase 
    {
        List<T> existingModels = InternalLoad<T>();

        if (existingModels.Contains(model))
            existingModels.Remove(model);
    }

    public void DeleteAllEntries<T>() where T : FileModelBase
    {
        InternalStore(new List<T>());
    }

    public bool EntryWithIdExists<T>(Guid id) where T : FileModelBase
    {
        List<T> existingModels = InternalLoad<T>();
        return existingModels.Any(x => x.Id == id);
    }


    private List<T> InternalLoad<T>() where T : FileModelBase
    {
        var filePath = GetFilePath<T>();

        if (!File.Exists(filePath))
            return new List<T>();

        var json = File.ReadAllText(filePath);
        var modelList = JsonSerializer.Deserialize<List<T>>(json);

        return modelList ?? new List<T>();
    }

    private void InternalStore<T>(List<T> entries) where T : FileModelBase
    {
        string jsonString = JsonSerializer.Serialize(entries, _jsonOptions);
        string filePath = GetFilePath<T>();
        File.WriteAllText(filePath, jsonString);
    }

    private string GetFilePath<T>() where T : FileModelBase
    {
        string format = "{0}\\{1}\\{2}.{3}";

        var fileExtension = _fileExtension;
        var directoryName = _directoryName;
        var fileName = GetFileName<T>();
        var directoryPath = GetAbsoluteDirectoryPath(_directoryPath);

        CheckAndCreateFolder(directoryPath + "\\" + directoryName);

        return string.Format(format, directoryPath, directoryName, fileName, fileExtension);
    }

    private string GetFileName<T>() where T : FileModelBase
    {
        return typeof(T).Name;
    }

    private string GetAbsoluteDirectoryPath(string? directoryPath)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            return Environment.CurrentDirectory;

        // If relative, convert it to an absolute path
        if (!Path.IsPathRooted(directoryPath))
            return Path.GetFullPath(directoryPath);

        return directoryPath;
    }

    private void CheckAndCreateFolder(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
    }
}
