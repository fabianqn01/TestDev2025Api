using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.Tests.Data
{
    public class AppDbContextTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")  // Base de datos en memoria
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void DbContext_Should_Create_Tables()
        {
            // Arrange
            using var context = GetDbContext();

            // Act
            context.Database.EnsureCreated(); // Asegura la creación de la base de datos en memoria

            // Assert
            Assert.NotNull(context.Users);    // Verifica que los DbSets no sean nulos
            Assert.NotNull(context.Entities);
        }

        [Fact]
        public void Can_Add_And_Retrieve_Employee()
        {
            // Arrange
            using var context = GetDbContext();
            var employee = new Employee { Name = "Test", Position = "Dev", Salary = 1000 };

            // Act
            context.Employees.Add(employee);
            context.SaveChanges();

            // Assert
            var retrieved = context.Employees.FirstOrDefault(e => e.Name == "Test");
            Assert.NotNull(retrieved);
            Assert.Equal("Dev", retrieved.Position);
        }
    }
}
