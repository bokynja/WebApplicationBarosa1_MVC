using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationBarosa.Models;

namespace WebApplicationBarosa.DataAccess.Repository.IRepository
{
    public interface IDogRepository : IRepository<Dog>
    {
        void Update(Dog obj);
    }
}
