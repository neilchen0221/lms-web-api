using Data.Database;
using Data.Repositories.Interfaces;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    class LecturerRepository : GenericRepository<Lecturer>, ILecturerRepository
    {
        public LecturerRepository(LMSEntities context) : base(context)
        {
        }
    }
}
