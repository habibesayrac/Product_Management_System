using PMS.DataAccessLayer.Abstract;
using PMS.DataAccessLayer.Concrete;

namespace PMS.DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public virtual void Delete(int id)
        {
            _context.Remove(id);
            _context.SaveChanges();
        }

        public virtual List<T> GetList()
        {
            return _context.Set<T>().ToList();
        }

        public virtual void Add(T t)
        {
            _context.Add(t);
            _context.SaveChanges();
        }

        public virtual bool Update(T t)
        {
            _context.Update(t);
            var value = _context.SaveChanges();
            if (value == null)
            {
                return false;
            }
            return true;
        }
    }
}