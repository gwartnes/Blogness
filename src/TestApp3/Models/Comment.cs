using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Blogness.Models.Repository.Interfaces;

namespace Blogness.Models
{
    public class Comment
    {
        public Comment()
        {
            Id = Guid.NewGuid().ToString();
            Approved = false;
        }

        public string HashEmail()
        {
            using (MD5 hasher = MD5.Create())
            {
                if (string.IsNullOrEmpty(Email))
                {
                    Email = "example@example.com";
                }
                var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(Email.Trim().ToLower()));
                var sb = new StringBuilder();
                foreach (var hashByte in hash)
                {
                    sb.Append(hashByte.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public string Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Author { get; set; }
        [EmailAddress]
        [Required]
        [Display(Name = "Email", Description = "Your email address will never be displayed")]
        public string Email { get; set; }
        [Url]
        public string Website { get; set; }
        [Display(Name = "Comment")]
        [StringLength(1000, MinimumLength = 5)]
        [Required]
        public string Content { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime DateCreated { get; set; }
        [BsonIgnore]
        public string PostId { get; set; }
        public bool Approved { get; set; }
    }
}