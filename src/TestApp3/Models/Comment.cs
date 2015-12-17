using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Models
{
    public class Comment
    {
        public string Author { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Content { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime DateCreated { get; set; }
    }
}