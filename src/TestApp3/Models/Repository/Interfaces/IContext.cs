using System.Threading.Tasks;

namespace TestApp3.Models.Repository.Interfaces
{
    public interface IContext
    {
        IDataSet<T> SetAsync<T>();
    }
}