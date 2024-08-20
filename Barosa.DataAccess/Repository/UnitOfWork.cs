using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationBarosa.DataAccess.Data;
using WebApplicationBarosa.DataAccess.Repository.IRepository;

namespace WebApplicationBarosa.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IDogRepository Dog { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Dog = new DogRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
