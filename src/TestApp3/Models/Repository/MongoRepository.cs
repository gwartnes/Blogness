using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestApp3.Models.Repository.Context;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Models.Repository
{
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IContext context)
        {
            _collection = context.Set<T>();
        }

        public bool Delete(T record)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetResults(Expression<Func<T, bool>> predicate = null, int limit = 0, int skip = 0)
        {
            return await _collection.Find(predicate).Skip(skip).Limit(limit).ToListAsync();
        }

        public bool Insert(T record)
        {
            throw new NotImplementedException();
        }

        public bool Update(T record)
        {
            throw new NotImplementedException();
        }
    }
}
