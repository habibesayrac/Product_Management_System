namespace PMS.DataAccessLayer.Abstract
{
    public interface IGenericRepository<T>
    {
        void Add(T t);
        bool Update(T t);
        void Delete(int id);
        List<T> GetList();
    }
}