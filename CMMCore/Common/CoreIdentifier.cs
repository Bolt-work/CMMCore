using CMMCore.Helpers;
using CMMCore.Managers;

namespace CMMCore.Common;

public class CoreIdentifier<T> where T : CoreIdentifier<T>, new()
{
    private readonly string _id;

    public CoreIdentifier()
    {
        _id = CoreHelper.NewId();
    }

    public CoreIdentifier(string? id)
    {
        if (string.IsNullOrEmpty(id))
            throw new IdArgumentStringIsNullOrEmptyCoreException<T>();

        _id = id;
    }

    public static T CreateNew()
    {
        return new T();
    }

    public string Id => _id;

    public override string ToString()
    {
        return _id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is CoreIdentifier<T> other)
        {
            return _id == other._id;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
}