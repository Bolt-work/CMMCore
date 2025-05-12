using CMMCore.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CMMCore.Models
{
    public abstract class CoreModelBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? KeyId { get; set; }

        [CoreId]
        public virtual string? Id { get; set; }
    }
}
