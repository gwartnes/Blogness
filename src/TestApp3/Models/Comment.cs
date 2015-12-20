﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Models
{
    public class Comment
    {
        public string HashEmail()
        {
            using (MD5 hasher = MD5.Create())
            {
                var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(Email.Trim().ToLower()));
                var sb = new StringBuilder();
                foreach (var hashByte in hash)
                {
                    sb.Append(hashByte.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public string Author { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Url]
        public string Website { get; set; }
        public string Content { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime DateCreated { get; set; }
    }
}