using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}