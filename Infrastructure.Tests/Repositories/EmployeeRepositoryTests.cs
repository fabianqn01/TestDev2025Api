using Infrastructure.Data;
using Infrastructure.Repositories;
using Domain.Entities;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.Tests.Repositories
{
    public class EmployeeRepositoryTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeTestDb")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetEmployeeAsync_ShouldReturnEmployee_WhenEmployeeExists()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EmployeeRepository(context);

            var employee = new Employee { Name = "John Doe", Position = "Developer", Salary = 5000m, EntityId = 1 };
            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetEmployeeAsync(employee.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public async Task GetEmployeeAsync_ShouldReturnNull_WhenEmployeeDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EmployeeRepository(context);

            // Act
            var result = await repository.GetEmployeeAsync(999);

            // Assert
            Assert.Null(result);
        }

        

        [Fact]
        public async Task CreateEmployeeAsync_ShouldAddEmployeeToDatabase()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EmployeeRepository(context);

            var employeeDTO = new EmployeeDTO
            {
                Name = "Alice Smith",
                Position = "Analyst",
                Salary = 6000m,
                EntityId = 1
            };

            // Act
            var result = await repository.CreateEmployeeAsync(employeeDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Alice Smith", result.Name);
            Assert.Equal(6000m, result.Salary);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ShouldUpdateEmployeeDetails()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EmployeeRepository(context);

            var employee = new Employee { Name = "Old Name", Position = "Old Position", Salary = 4000m, EntityId = 1 };
            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            var updatedDTO = new EmployeeDTO
            {
                Name = "New Name",
                Position = "New Position",
                Salary = 8000m,
                EntityId = 2
            };

            // Act
            var result = await repository.UpdateEmployeeAsync(employee.Id, updatedDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Name", result.Name);
            Assert.Equal("New Position", result.Position);
            Assert.Equal(8000m, result.Salary);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ShouldReturnNull_WhenEmployeeDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EmployeeRepository(context);

            var updatedDTO = new EmployeeDTO
            {
                Name = "Non-Existent",
                Position = "Non-Existent",
                Salary = 5000m,
                EntityId = 1
            };

            // Act
            var result = await repository.UpdateEmployeeAsync(999, updatedDTO);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ShouldRemoveEmployeeFromDatabase()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EmployeeRepository(context);

            var employee = new Employee { Name = "To Delete", Position = "To Remove", Salary = 4500m, EntityId = 1 };
            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.DeleteEmployeeAsync(employee.Id);

            // Assert
            Assert.True(result);
            Assert.Null(await context.Employees.FindAsync(employee.Id));
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ShouldReturnFalse_WhenEmployeeDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EmployeeRepository(context);

            // Act
            var result = await repository.DeleteEmployeeAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}
