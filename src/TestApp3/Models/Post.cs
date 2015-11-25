﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Models
{
    public class Post
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ObjectId UserId { get; set; }
        [BsonIgnore]
        public User User { get; set; }
        public string[] Tags { get; set; }
        [BsonDateTimeOptions (Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime DatePublished { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime DateUpdated { get; set; }
        public Comment[] Comments { get; set; }

    }
}
