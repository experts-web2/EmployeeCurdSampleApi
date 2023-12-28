using Domain.DAL.GenericRepo;
using Domain.DAL.Interface;
using Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.Absraction
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
                
        }
    }

}
