
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
            var objFromDb = _db.Dogs.FirstOrDefault(u=> u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.Breed = obj.Breed;
                objFromDb.Description = obj.Description;
                objFromDb.SKU = obj.SKU;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Gender = obj.Gender;
                objFromDb.CategoryId = obj.CategoryId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
