using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace StockAppli.Models.Entities
{
    [BsonIgnoreExtraElements]
    public abstract class BaseEntity
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("createdAt")]
        public DateTime? CreatedAt { get; set; }
        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UpdaterId { get; set; }
        [BsonElement("deletedAt")]
        public DateTime? DeletedAt { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeleterId { get; set; }
        [BsonElement("enable")]
        public bool Enable { get; set; } = true;
    }
}
