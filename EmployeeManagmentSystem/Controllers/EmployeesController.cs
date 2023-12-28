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
            try
            {
                var response = await _employeeService.AddAsync(employeeDto);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Error occurred in adding employee.");
            }
           
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var response = _employeeService.GetAll();
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Error occurred in fetching employees list.");
            }
            
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            try
            {
                var response = await _employeeService.GetByIdAsync(Id);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Error occurred in fetching employee.");
            }
            
        }

        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> UpdateEmployee(int Id, [FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var response = await _employeeService.Update(Id, employeeDto);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Error occurred in update employee.");
            }
           
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            try
            {
                var result = await _employeeService.Delete(Id);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Error occurred in deleting employee.");
            }
        
        }
    }
}
