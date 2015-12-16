﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TestApp3.Models.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using TestApp3.Models.Interfaces;
using MarkdownSharp;

namespace TestApp3.Models
{
    public class Post : IEntity
    {
        private string _content;

        public Post()
        {
            DatePublished = DateUpdated = DateTime.Now;
            Id = ObjectId.GenerateNewId().ToString();
        }

        public string GetPreview()
        {
            if (Content.Length >= 100)
            {
                return Content.Substring(0, 100) + "...";
            }
            return Content;
        }

        public async Task<Post> SetUser(UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync(UserName);
            User = user == null ? new User { UserName = "Default" } : user;
            return this;
        }


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content {
            get
            {
                var markdown = new Markdown();
                return (new Markdown()).Transform(_content);
            }
            set
            {
                _content = value;
            }
        }
        public string UserName { get; set; }
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
