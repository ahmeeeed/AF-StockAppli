using MongoDB.Bson.Serialization.Attributes;
using static StockAppli.Models.Enums;

namespace StockAppli.Models.Entities
{
    public class User : BaseEntity
    {
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("firstName")]
        public string? FirstName { get; set; }

        [BsonElement("lastName")]
        public string? LastName { get; set; }
        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }

        [BsonElement("passwordSalt")]
        public string PasswordSalt { get; set; }
        [BsonElement("address")]
        public string? Address { get; set; }

        [BsonElement("zipCode")]
        public string? ZipCode { get; set; }

        [BsonElement("city")]
        public string? City { get; set; }

        [BsonElement("country")]
        public string? Country { get; set; }
        [BsonElement("phone")]
        public string? Phone { get; set; }
        [BsonElement("accountVerified")]
        public bool? AccountVerified { get; set; }
        [BsonElement("permissions")]
        public List<Permission>? Permissions { get; set; }
    }
}
