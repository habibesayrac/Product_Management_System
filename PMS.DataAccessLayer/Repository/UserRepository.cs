using PMS.DataAccessLayer.Abstract;
using PMS.DataAccessLayer.Concrete;
using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly Context _dbContext;
        public UserRepository(Context context) : base(context)
        {
            _dbContext = context;
        }

        public User GetById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(x=>x.UserId == id);
            return user != null ? user : null;
        }
        
        public override void Add(User user)
        {
            if (user.UserId == 0 || user.UserId == null)
            {
                var maxUserId = _dbContext.Users.Max(p => (int?)p.UserId) ?? 0;
                user.UserId = maxUserId + 1;
            }

            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }
        public override void Delete(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == id) ?? throw new Exception("Hatalı ID girişi: Sipariş bulunamadı.");
            _dbContext.Remove(user);
            _dbContext.SaveChanges();
        }
        public override bool Update(User t)
        {
            var updatedUser = GetById(t.UserId);
            if (updatedUser == null)
            {
                return false;
            }

            return base.Update(updatedUser);
        }
    }
}
