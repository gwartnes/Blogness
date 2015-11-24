using System.Threading.Tasks;

namespace TestApp3.Models.Repository.Interfaces
{
    public interface IContext
    {
        object SetAsync<T>();
    }
}