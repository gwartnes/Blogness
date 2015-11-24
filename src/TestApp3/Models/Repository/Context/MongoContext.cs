﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TestApp3.Models.Repository.Interfaces;
using Microsoft.Framework.OptionsModel;
using Inflector;
using System.Threading;

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
        public object SetAsync<T>()
        {
            // get the name of the POCO class to serialize to a mongo collection, then modify it to adhere to standard naming conventions (lower-case/plural)
            // Unnecessary, of course, but fun.
            Inflector.Inflector.SetDefaultCultureFunc = () => Thread.CurrentThread.CurrentUICulture;
            var collectionName = typeof(T).Name.ToLower().Pluralize();

            var set = _database.GetCollection<T>(collectionName);

            if (set == null)
            {
                _database.CreateCollectionAsync(collectionName);
                set = _database.GetCollection<T>(collectionName);
            }
            return set;
        }
    }
}
