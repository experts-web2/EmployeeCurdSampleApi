using Domain.DAL.GenericRepo;
using Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.Interface
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    }
}
