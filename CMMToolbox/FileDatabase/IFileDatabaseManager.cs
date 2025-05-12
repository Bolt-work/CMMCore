namespace CMMFileDatabase.FileDatabase
{
    public interface IFileDatabaseManager
    {
        void DeleteAllEntries<T>() where T : FileModelBase;
        void DeleteEntry<T>(Guid id) where T : FileModelBase;
        void DeleteEntry<T>(T model) where T : FileModelBase;
        bool EntryWithIdExists<T>(Guid id) where T : FileModelBase;
        IEnumerable<T> GetAllEntries<T>() where T : FileModelBase;
        IEnumerable<T> GetByKeyStr<T>(string key, string value) where T : FileModelBase;
        T? GetEntryById<T>(Guid id) where T : FileModelBase;
        T? GetNewest<T>() where T : FileModelBase;
        T? GetOldest<T>() where T : FileModelBase;
        void Upsert<T>(T model) where T : FileModelBase;
    }
}