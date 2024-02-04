using Gotchi.Core.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gotchi.Core.Models
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
