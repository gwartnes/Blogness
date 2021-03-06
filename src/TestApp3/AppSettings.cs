﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogness
{
    public class AppSettings
    {
        public AppSettings() { }
        public MongoDBConfig MongoDBConfig { get; set; }

    }
    public class MongoDBConfig
    {
        public MongoDBConfig() { }
        public MongoContextDetails MongoContextDetails { get; set; }

    }
    public class MongoContextDetails
    {
        public MongoContextDetails() { }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
