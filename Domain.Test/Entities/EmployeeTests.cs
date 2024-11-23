using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Entities
{
    public class EmployeeTests
    {
        [Fact]
        public void Employee_Should_Have_Null_Entity_Initially()
        {
            // Arrange & Act
            var employee = new Employee();

            // Assert
            Assert.Null(employee.Entity); // Entity should be null initially
        }

        [Fact]
        public void Employee_Should_Set_And_Get_Entity_Correctly()
        {
            // Arrange
            var entity = new Entity { Id = 1, Name = "Tech Corp", Description = "A leading tech company" };
            var employee = new Employee { Entity = entity };

            // Act & Assert
            Assert.Equal(entity, employee.Entity);
            Assert.Equal(1, employee.Entity.Id);
            Assert.Equal("Tech Corp", employee.Entity.Name);
        }
    }
}
