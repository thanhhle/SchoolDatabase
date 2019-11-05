using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StudentAddressRepository : Repository<StudentAddress>, IStudentAddressRepository
    {
        public StudentAddressRepository() : base(new SchoolDBEntities())
        {

        }
    }
}
