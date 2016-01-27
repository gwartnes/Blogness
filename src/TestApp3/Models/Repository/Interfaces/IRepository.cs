using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogness.Models.Repository.Interfaces
{
    public interface IRepository<T> 
    {
        Task<IEnumerable<T>> GetResults(Expression<Func<T, bool>> predicate = null, int limit = 0, int skip = 0);
        Task<bool> InsertAsync(T record);
        Task<bool> UpdateAsync(T record);
        Task<bool> DeleteAsync(T record);
    }
}