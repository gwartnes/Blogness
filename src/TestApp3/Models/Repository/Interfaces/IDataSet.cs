using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp3.Models.Repository.Interfaces
{
    public interface IDataSet<T> : IMongoCollection<T>
    {
    }
}
