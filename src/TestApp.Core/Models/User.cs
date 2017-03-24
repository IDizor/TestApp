using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TestApp.Core.Interfaces.Data;

namespace TestApp.Core.Models
{
    public class User : IDocument
    {
        public ObjectId Id { get; set; }

        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("Tags")]
        public string[] Tags { get; set; }
    }
}
