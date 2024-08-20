
using WebApplicationBarosa.DataAccess.Data;
using WebApplicationBarosa.DataAccess.Repository.IRepository;
using WebApplicationBarosa.Models;

namespace WebApplicationBarosa.DataAccess.Repository
{
    public class DogRepository : Repository<Dog>, IDogRepository
    {
        private ApplicationDbContext _db;
        public DogRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Dog obj)
        {
            _db.Dogs.Update(obj);
        }
    }
}
