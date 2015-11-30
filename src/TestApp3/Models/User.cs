using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Models
{
    public class User : IUser<string>
    {
        public User()
        {
            Id = ObjectId.GenerateNewId().ToString();
            AccessFailedCount = 0;
            LockoutEnabled = false;
        }

        public bool HasPassword()
        {
            return PasswordHash != null;
        }
        public void AddRole(string role)
        {
            if (!IsInRole(role))
            {
                Roles.Add(role);
            }
        }
        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }
        public void RemoveFromRole(string role)
        {
            if (IsInRole(role))
            {
                Roles.RemoveAll(r => string.Equals(r.ToLower(), role.ToLower()));
            }
        }
        public int IncrementAccessFailedCount()
        {
            return AccessFailedCount++;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set;}
        public List<string> Roles { get; set; }
        public string PasswordHash { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime LockoutEndDate { get; set; }
        public string SecurityStamp { get; set; }


    }
}