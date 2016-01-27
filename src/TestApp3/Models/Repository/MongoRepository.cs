using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blogness.Models.Interfaces;
using Blogness.Models.Repository.Context;
using Blogness.Models.Repository.Interfaces;

namespace Blogness.Models.Repository
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
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
            bool succeeded;

            var updateFilter = Builders<T>.Filter.Where(f => f.Id == record.Id);
            try
            {
                await _collection.ReplaceOneAsync(updateFilter, record);
                succeeded = true;
            }
            catch (Exception)
            {
                succeeded = false;
            }
            return succeeded;
        }
    }
}
