using WebApplicationBarosa.DataAccess.Data;
using WebApplicationBarosa.DataAccess.Repository.IRepository;
using WebApplicationBarosa.Models;
using System.Linq;

namespace WebApplicationBarosa.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public ApplicationUser GetByEmail(string email)
        {
            return _db.ApplicationUsers.FirstOrDefault(u => u.Email == email);
        }
    }
}
