using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TestApp3.Models.Repository.Interfaces;
using Microsoft.Framework.OptionsModel;

namespace TestApp3.Models.Repository.Context
{
    public class MongoContext : IContext
    {

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<AppSettings> configuration)
        {
            _client = new MongoClient(configuration.Options.MongoDBConfig.MongoContextDetails.ConnectionString);
            _database = _client.GetDatabase(configuration.Options.MongoDBConfig.MongoContextDetails.DatabaseName);
        }
        public IDataSet<T> Set<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name) as IDataSet<T>;
        }
    }
}
