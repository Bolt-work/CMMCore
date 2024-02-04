using Gotchi.Core.Models;
using MongoDB.Driver;


namespace Gotchi.Core.Repository;
public abstract class RepositoryBase<T> where T : CoreModelBase
{
    protected string _connectionString;
    protected string _databaseName;
    protected string _collectionName;

    public RepositoryBase(RepositorySettings repositorySettings)
    {
        _connectionString = repositorySettings.ConnectionString;
        _databaseName = repositorySettings.DatabaseName;
        _collectionName = repositorySettings.CollectionName;
    }

    protected IMongoCollection<T> ConnectToMongo()
    {
        var client = new MongoClient(_connectionString);
        var db = client.GetDatabase(_databaseName);
        return db.GetCollection<T>(_collectionName);
    }

    protected string GetCoreId(T model)
    {
        if (model is null)
            throw new ArgumentNullException("Mandatory parameter", nameof(model));
        
        foreach (var property in model.GetType().GetProperties())
        {
            foreach (var attribute in property.GetCustomAttributes(true))
            {
                if (attribute.GetType() == typeof(CoreId))
                {
                    var value = property.GetValue(model);
                    return Convert.ToString(value) ?? throw new NullReferenceException();
                }
            }
        }

        throw new CoreIdAttributeNotFoundException(model);
    }

    protected bool UpsertEntry(T model)
    {
        var id = GetCoreId(model);
        var result = ConnectToMongo().ReplaceOne(FilterId(id), model, new ReplaceOptions { IsUpsert = true });
        return result.IsAcknowledged;
    }

    protected bool DeleteEntry(string? id)
    {
        id = id ?? throw new ArgumentNullException(id);
        var result = ConnectToMongo().DeleteOne(FilterId(id));
        return result.IsAcknowledged;
    }

    protected bool DeleteAllEntries()
    {
        var filter = Builders<T>.Filter.Where(_ => true);
        var result = ConnectToMongo().DeleteMany(filter);
        return result.IsAcknowledged;
    }

    protected T GetEntryById(string id)
    {
        return ConnectToMongo().Find(x => x.Id == id).FirstOrDefault();
    }

    protected async Task<T> GetEntryByIdAsync(string id)
    {
        return await ConnectToMongo().Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    protected bool EntryExists(string id)
    {
        return ConnectToMongo().Find(x => x.Id == id).Any();
    }

    protected bool EntryExistsByKey(string key, string value)
    {
        return ConnectToMongo().Find(Filter(key, value)).Any();
    }

    protected async Task<bool> EntryExistsByKeyAsync(string key, string value)
    {
        return await ConnectToMongo().Find(Filter(key, value)).AnyAsync();
    }

    protected bool EntriesAny() 
    {
        return ConnectToMongo().Find(_ => true).Any();
    }

    protected async Task<bool> EntriesAnyAsync()
    {
        return await ConnectToMongo().Find(_ => true).AnyAsync();
    }

    protected ICollection<T> GetAllEntries()
    {
        return ConnectToMongo().Find(_ => true).ToList();
    }
    protected async Task<ICollection<T>> GetAllEntriesAsync()
    {
        return await ConnectToMongo().Find(_ => true).ToListAsync();
    }

    protected T GetByKeyStr(string key, string value)
    {
        return ConnectToMongo().Find(Filter(key, value)).SingleOrDefault();
    }

    protected async Task<T> GetByKeyStrAsync(string key, string value)
    {
        return await ConnectToMongo().Find(Filter(key, value)).SingleOrDefaultAsync();
    }

    protected ICollection<T> GetManyByKeyStr(string key, string value)
    {
        return ConnectToMongo().Find(Filter(key, value)).ToList();
    }

    protected async Task<ICollection<T>> GetManyByKeyStrAsync(string key, string value)
    {
        return await ConnectToMongo().Find(Filter(key, value)).ToListAsync();
    }

    protected FilterDefinition<T> FilterId(string id) => Filter("Id", id);
    protected FilterDefinition<T> Filter(string key, string id)
    {
        return Builders<T>.Filter.Eq(key, id);
    }
}
