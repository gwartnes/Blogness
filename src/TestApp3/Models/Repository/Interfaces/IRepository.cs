using System;
using System.Collections.Generic;

namespace TestApp3.Models.Repository.Interfaces
{
    public interface IRepository<T> where T : class //very generic, might step it up closer to what is needed
    {
        IEnumerable<T> GetResults(Func<T, bool> predicate = null, int limit = 0, int skip = 0);
        bool Insert(T record);
        bool Update(T record);
        bool Delete(T record);
    }
}