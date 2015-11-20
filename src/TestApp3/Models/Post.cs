using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestApp3.Models
{
    public class Post
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public User Author { get; set; }
        public string[] Tags { get; set; }
        [BsonDateTimeOptions (Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime DatePublishedUtc { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime DateUpdatedUtc { get; set; }
        public Comment[] Comments { get; set; }

    }
}
