using MongoDB.Bson.Serialization.Attributes;

namespace StockAppli.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class Session : BaseEntity
    {
        [BsonElement("idUser")]
        public string IdUser { get; set; }

        [BsonElement("token")]
        public string Token { get; set; }
    }
}
