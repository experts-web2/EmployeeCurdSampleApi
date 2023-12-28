using Domain.BL.Services.Interfaces;
using Domain.DAL.GenericRepo;
using Domian.Entities;
using Domian.Entities.Dto;

namespace Domain.BL.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _repository;
        public EmployeeService(IGenericRepository<Employee> repository)
        {
            _repository = repository;
        }
        public async Task<EmployeeDto> AddAsync(EmployeeDto employeeDto)
        {
            var entity = SetEntity(employeeDto);
            await _repository.Add(entity);
            _repository.SaveChange();
            return employeeDto;
        }

        public async Task<string> Delete(int id)
        {
            var employee = await _repository.GetByID(id);
            if (employee == null)
            {
                return "No Record found";
            }
            _repository.Delete(id);
            _repository.SaveChange();
            return "Deleted Record";
        }

        public List<EmployeeDto> GetAll()
        {
            var employeeList = _repository.GetAll().ToList();
            if (employeeList is null)
            {
                return new List<EmployeeDto>();
            }

            return employeeList.Select(SetDto).ToList();
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByID(id);
            if (employee is null)
            {
                return null;
            }

            return SetDto(employee);
        }

        public async Task<EmployeeDto> Update(int Id, EmployeeDto employeeDto)
        {
            var employee = await _repository.GetByID(Id);
            if (employee != null)
            {
                employee = SetEntity(employeeDto, employee);
                var updatedEmployee = _repository.update(employee);
                _repository.SaveChange();
                return SetDto(updatedEmployee);
            }

            return new();
        }

        private static EmployeeDto SetDto(Employee employee)
        {
            return new()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
            };
        }

        private static Employee SetEntity(EmployeeDto employeeDto, Employee? employee = null)
        {
            if (employee is null)
            {
                employee = new()
                {
                    CreatedDate = DateTime.UtcNow,
                };
            }
            else
            {
                employee.ModifiedDate = DateTime.UtcNow;
            }

            employee.FirstName = employeeDto.FirstName;
            employee.MiddleName = employeeDto.MiddleName;
            employee.LastName = employeeDto.LastName;
            return employee;
        }
    }
}