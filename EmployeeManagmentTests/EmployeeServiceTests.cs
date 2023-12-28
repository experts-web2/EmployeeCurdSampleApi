using Domain.BL.Services.Implementation;
using Domain.BL.Services.Interfaces;
using Domain.DAL;
using Domain.DAL.GenericRepo;
using Domian.Entities;
using Domian.Entities.Dto;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentTests
{
    public class EmployeeServiceTests
    {
        private IEmployeeService employeeService;
        private Mock<IGenericRepository<Employee>> mockRepository;
        public EmployeeServiceTests()
        {
            mockRepository = new Mock<IGenericRepository<Employee>>();
            employeeService = new EmployeeService(mockRepository.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEmployeeToRepository()
        {
            var Model = new EmployeeDto()
            { 
                Id = 10,
                FirstName = "Ali",
                LastName ="Test",
                MiddleName ="Test",
            };

            var result  =await employeeService.AddAsync(Model);

            mockRepository.Verify(repo => repo.Add(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnsEmployeeDtos_WhenEmployeesExist()
        {
            // Arrange
            var repositoryMock = new Mock<IGenericRepository<Employee>>();
            var employeeService = new EmployeeService(repositoryMock.Object); 

            var employees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "John", MiddleName = "Doe" },
            new Employee { Id = 2, FirstName = "Jane", MiddleName = "josay"}
        };

            repositoryMock.Setup(repo => repo.GetAll()).Returns(employees.AsQueryable());

            // Act
            var result = employeeService.GetAll();

            // Assert
            Assert.Contains(result, x => x.FirstName == "John");
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count); 

        }

       


        [Fact]
        public async Task Delete_ReturnsDeletedMessage_WhenEmployeeExists()
        {
            // Arrange
            var repositoryMock = new Mock<IGenericRepository<Employee>>();
            var employeeService = new EmployeeService(repositoryMock.Object);

            var employee = new Employee { Id = 1, FirstName = "John", MiddleName = "Doe" };

            repositoryMock.Setup(repo => repo.GetByID(employee.Id)).ReturnsAsync(employee);

            // Act
            var result = await employeeService.Delete(employee.Id);

            // Assert
            Assert.Equal("Deleted Record", result);
            repositoryMock.Verify(repo => repo.Delete(employee.Id), Times.Once);
            repositoryMock.Verify(repo => repo.SaveChange(), Times.Once);
        }
    }
}
