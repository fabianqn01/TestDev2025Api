using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Entities
{
    public class ApplicationUserTests
    {
        [Fact]
        public void ApplicationUser_Should_Initialize_Roles_As_Empty_List()
        {
            // Arrange & Act
            var user = new ApplicationUser();

            // Assert
            Assert.NotNull(user.Roles);
            Assert.Empty(user.Roles);
        }

        [Fact]
        public void ApplicationUser_Should_Set_And_Get_Name_Correctly()
        {
            // Arrange
            var user = new ApplicationUser { Name = "John Doe" };

            // Act & Assert
            Assert.Equal("John Doe", user.Name);
        }

        [Fact]
        public void ApplicationUser_Should_Set_And_Get_Email_Correctly()
        {
            // Arrange
            var user = new ApplicationUser { Email = "john.doe@example.com" };

            // Act & Assert
            Assert.Equal("john.doe@example.com", user.Email);
        }

        [Fact]
        public void ApplicationUser_Should_Set_And_Get_Password_Correctly()
        {
            // Arrange
            var user = new ApplicationUser { Password = "password123" };

            // Act & Assert
            Assert.Equal("password123", user.Password);
        }
    }
}
