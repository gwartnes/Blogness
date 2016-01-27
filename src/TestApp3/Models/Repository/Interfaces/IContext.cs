using System.Threading.Tasks;

namespace Blogness.Models.Repository.Interfaces
{
    public interface IContext
    {
        object SetAsync<T>();
    }
}