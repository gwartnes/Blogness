using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TestApp3.Models
{
    public class Comment
    {
        public string Author { get; set; }
        public string Content { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime CreatedAtUtc { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime DateUpdatedUtc { get; set; }
    }
}