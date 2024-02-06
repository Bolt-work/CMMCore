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
        public string? Id { get; set; }
    }
}
