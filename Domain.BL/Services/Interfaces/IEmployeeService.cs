using Domian.Entities.Dto;
namespace Domain.BL.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeDto?> GetByIdAsync(int id);
    List<EmployeeDto> GetAll();
    Task<EmployeeDto> AddAsync(EmployeeDto employeeDto);
    Task<EmployeeDto> Update(int Id, EmployeeDto employeeDto);
    Task<string> Delete(int id);
}