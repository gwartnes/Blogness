using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MausWorks.MongoDB;
using MongoDB.Driver;
using TestApp3.Models.Repository.Interfaces;
using Microsoft.Framework.OptionsModel;

namespace TestApp3.Models.Repository.Context
{
    public class MongoContext : IContext
    {
        
        public IMongoCollection<Post> Posts { get; set; }
        public IMongoCollection<Comment> Comments { get; set; }

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;


        public MongoContext(IOptions<AppSettings> configuration)
        {
            _client = new MongoClient(configuration.Options.MongoDBConfig.MongoContextDetails.ConnectionString);
        }
        public void SetupCollections()
        {
            //Posts = CreateCollection<Post>("Posts");
            //Comments = CreateCollection<Comment>("Comments");
        }
    }
}
