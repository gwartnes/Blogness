using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApp3.Models;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Models
{
    public class User
    {
        public User()
        {
            Id = ObjectId.GenerateNewId().ToString();
            AccessFailedCount = 0;
            LockoutEnabled = false;
            LockoutEndDate = DateTime.MinValue;
            Roles = new List<Role>();
            AddRole("user");
            UserCreatedDate = LastLoginDate = DateTime.Now;
        }

        public bool HasPassword()
        {
            return PasswordHash != null;
        }
        public void AddRole(string role)
        {
            if (!IsInRole(role))
            {
                Roles.Add(new Role { RoleName = role });
            }
        }
        public bool IsInRole(string role)
        {
            return Roles.Any(r => r.RoleName == role); 
        }
        public void RemoveFromRole(string role)
        {
            if (IsInRole(role))
            {
                Roles.RemoveAll(r => string.Equals(r.RoleName.ToLower(), role.ToLower()));
            }
        }
        public int IncrementAccessFailedCount()
        {
            return AccessFailedCount++;
        }

        public IList<string> GetRoles()
        {
            var roleList = new List<string>();
            foreach (var role in Roles)
            {
                roleList.Add(role.RoleName);
            }
            return roleList;
        }

        public string GetFullName()
        {
            if (FirstName != null && LastName != null)
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
            else
            {
                return UserName;
            }
        }

        public string GetFirstName()
        {
            return FirstName != null ? FirstName : UserName;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set;} 
        public List<Role> Roles { get; set; }
        public string PasswordHash { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime LockoutEndDate { get; set; }
        public string SecurityStamp { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime UserCreatedDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime LastLoginDate { get; set; }
    }
}