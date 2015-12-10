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
            _collection = context.SetAsync<T>() as IMongoCollection<T>;
        }

        public async Task<bool> DeleteAsync(T record)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetResults(Expression<Func<T, bool>> predicate = null, int limit = 10, int skip = 0)
        {            
            if (predicate == null)
            {
                predicate = all => true;
            }
            return await _collection.Find(predicate).Skip(skip).Limit(limit).ToListAsync();
        }

        public async Task<bool> InsertAsync(T record)
        {
            bool succeeded;
            try
            {
                await _collection.InsertOneAsync(record);
                succeeded = true;
            }
            catch (Exception)
            {
                succeeded = false;
            }
            return succeeded;
        }

        public async Task<bool> UpdateAsync(T record)
        {
            throw new NotImplementedException();
        }
    }
}
