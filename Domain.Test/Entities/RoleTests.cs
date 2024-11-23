using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Entities
{
    public class RoleTests
    {
        [Fact]
        public void Role_Should_Initialize_Users_As_Empty_List()
        {
            // Arrange & Act
            var role = new Role();

            // Assert
            Assert.NotNull(role.Users);
            Assert.Empty(role.Users);
        }

        [Fact]
        public void Role_Should_Set_And_Get_Name_Correctly()
        {
            // Arrange
            var role = new Role { Name = "Administrator" };

            // Act & Assert
            Assert.Equal("Administrator", role.Name);
        }
    }
}
