using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Models.Repository
{
    public class MongoPostRepository : IRepository<Post>
    {
        public MongoPostRepository(IContext context)
        {
            Context = context;
        }
        public IContext Context { get; set; }

        public bool Delete(Post record)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetResults(Func<Post, bool> predicate = null, int limit = 0, int skip = 0)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Post record)
        {
            throw new NotImplementedException();
        }

        public bool Update(Post record)
        {
            throw new NotImplementedException();
        }
    }
}
