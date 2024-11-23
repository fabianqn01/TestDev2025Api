using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test.Controllers
{
    public class UserControllerTests : WebApiTestsBase
    {
        [Fact]
        public async Task LoginUser_WithValidCredentials_ShouldReturnOk()
        {
            // Arrange
            var loginDTO = new
            {
                Email = "user@example.com",
                Password = "string"
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginDTO), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user/login", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("token", responseString.ToLower());  // Verifica si la respuesta contiene un token JWT
        }

        [Fact]
        public async Task LoginUser_WithInvalidCredentials_ShouldReturnUnauthorized()
        {
            // Arrange
            var loginDTO = new
            {
                Email = "user@invalid.com",
                Password = "stringInvalid"
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginDTO), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user/login", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);  // Asumiendo que devuelve 400 en credenciales incorrectas
        }

        [Fact]
        public async Task RegisterUser_WithValidData_ShouldReturnOk()
        {
            // Arrange
            // Generar valores aleatorios para Name y Email
            var randomName = "user" + Guid.NewGuid().ToString("N"); // Genera un nombre único
            var randomEmail = randomName + "@example.com"; // Genera un email único basándose en el nombre aleatorio

            var registerDTO = new
            {
                Name = randomName, // Nombre aleatorio
                Email = randomEmail, // Email aleatorio
                Password = "NewUserPassword123!", // Contraseña
                ConfirmPassword = "NewUserPassword123!", // Confirmación de la contraseña
                RoleIds = new List<int> { 1, 2 } // Lista de roles (puedes ajustarla o dejarla vacía)
            };

            var content = new StringContent(JsonConvert.SerializeObject(registerDTO), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user/register", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task RegisterUser_WithInvalidData_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidRegisterDTO = new
            {
                // Falta el campo 'Username' requerido
                Password = "Password123!",
                Email = "invaliduser@example.com"
            };

            var content = new StringContent(JsonConvert.SerializeObject(invalidRegisterDTO), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user/register", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task RegisterUser_WithExistingUsername_ShouldReturnBadRequest()
        {
            // Arrange
            var existingUserDTO = new
            {
                Username = "existinguser",  // Asume que este usuario ya existe en la base de datos
                Password = "ExistingUserPassword123!",
                Email = "existinguser@example.com"
            };

            var content = new StringContent(JsonConvert.SerializeObject(existingUserDTO), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user/register", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);  // El sistema debería devolver un error si el usuario ya existe
        }
    }
}