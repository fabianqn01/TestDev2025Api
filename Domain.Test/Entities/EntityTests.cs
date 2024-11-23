using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Entities
{
    public class EntityTests
    {
        [Fact]
        public void Entity_Should_Initialize_Employees_As_Empty_List()
        {
            // Arrange & Act
            var entity = new Entity();

            // Assert
            Assert.NotNull(entity.Employees);
            Assert.Empty(entity.Employees);
        }

        [Fact]
        public void Entity_Should_Set_And_Get_Name_Correctly()
        {
            // Arrange
            var entity = new Entity { Name = "Tech Corp" };

            // Act & Assert
            Assert.Equal("Tech Corp", entity.Name);
        }

        [Fact]
        public void Entity_Should_Set_And_Get_Description_Correctly()
        {
            // Arrange
            var entity = new Entity { Description = "A leading tech company" };

            // Act & Assert
            Assert.Equal("A leading tech company", entity.Description);
        }
    }
}
