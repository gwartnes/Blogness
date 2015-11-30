using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestApp3.Models.Repository.Interfaces
{
    public interface IRepository<T> 
    {
        Task<IEnumerable<T>> GetResults(Expression<Func<T, bool>> predicate = null, int limit = 0, int skip = 0);
        bool Insert(T record);
        bool Update(T record);
        bool Delete(T record);
    }
}