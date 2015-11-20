using MongoDB.Bson;

namespace TestApp3.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}