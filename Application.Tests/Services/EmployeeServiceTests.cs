using Application.Common.Exceptions;
using Application.Contracts;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly EmployeeService _service;

        public EmployeeServiceTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetEmployeeAsync_ReturnsEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employeeId = 1;
            var employee = new Employee { Id = employeeId, Name = "John Doe", Position = "Developer", Salary = 50000, EntityId = 1 };
            _mockRepo.Setup(repo => repo.GetEmployeeAsync(employeeId))
                     .ReturnsAsync(employee);

            // Act
            var result = await _service.GetEmployeeAsync(employeeId);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("John Doe");
            _mockRepo.Verify(repo => repo.GetEmployeeAsync(employeeId), Times.Once);
        }

        [Fact]
        public async Task GetEmployeeAsync_ThrowsNotFoundException_WhenEmployeeDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetEmployeeAsync(It.IsAny<int>()))
                     .ReturnsAsync((Employee)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetEmployeeAsync(1));
        }
    }
}
