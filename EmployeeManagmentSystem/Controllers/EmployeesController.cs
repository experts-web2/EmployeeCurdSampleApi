using Domain.BL.Services.Interfaces;
using Domian.Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDto employeeDto)
        {
            var response = await _employeeService.AddAsync(employeeDto);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _employeeService.GetAll();
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            var response = await _employeeService.GetByIdAsync(Id);
            return Ok(response);
        }

        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> UpdateEmployee(int Id, [FromBody] EmployeeDto employeeDto)
        {
            var response = await _employeeService.Update(Id, employeeDto);
            return Ok(response);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            var result = await _employeeService.Delete(Id);
            return Ok(result);
        }
    }
}
