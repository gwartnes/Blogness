namespace TestApp3.Models.Repository.Interfaces
{
    public interface IContext
    {
        IDataSet<T> Set<T>();
    }
}