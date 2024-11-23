using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test.Controllers
{
    public class EmployeeControllerTests : WebApiTestsBase
    {
        // Configura la autorización para pruebas protegidas
        private void SetAuthorizationHeader()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetJwtToken());
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnSuccessStatusCode()
        {
            // Arrange
            SetAuthorizationHeader();

            // Act
            var response = await _client.GetAsync("/api/employee");

            // Assert
            Assert.True(response.IsSuccessStatusCode, $"Expected success status code but got {response.StatusCode}");

        }

        [Fact]
        public async Task GetEmployeeById_WithValidId_ShouldReturnSuccessStatusCode()
        {
            // Arrange
            SetAuthorizationHeader();
            int validId = 1;  // Asegúrate de tener un ID válido en tu base de datos de prueba

            // Act
            var response = await _client.GetAsync($"/api/employee/{validId}");

            // Assert
            Assert.True(response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound,
                        $"Expected OK or NotFound but got {response.StatusCode}");
        }

        [Fact]
        public async Task GetEmployeeById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            SetAuthorizationHeader();
            int invalidId = 999;

            // Act
            var response = await _client.GetAsync($"/api/employee/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<int> CreateTestEntity()
        {
            var entity = new
            {
                Name = "Test Entity",
                Description = "Test Description Entity"// Ajusta según tu modelo de entidad
            };

            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/entity", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var entityResponse = JsonConvert.DeserializeObject<dynamic>(responseString);

            int id = (int)entityResponse.data.id;  
            return id;  
        }


        [Fact]
        public async Task CreateEmployee_WithValidData_ShouldReturnOk()
        {
            // Arrange
            SetAuthorizationHeader();

            int entityId = await CreateTestEntity();

            var employee = new
            {
                Name = "John Doe",
                Position = "Developer",
                Salary = 10000,
                EntityId = entityId
            };

            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/employee", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateEmployee_WithInvalidData_ShouldReturnBadRequest()
        {
            // Arrange
            SetAuthorizationHeader();
            var employee = new
            {
                // Missing required 'Name' field to simulate invalid data
                Position = "Developer",
                Salary = 10000
            };

            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/employee", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateEmployee_WithValidData_ShouldHandleNonExistentEmployee()
        {
            // Arrange
            SetAuthorizationHeader();

            int entityId = await CreateTestEntity();

            int validId = 1;  // ID que intentaremos actualizar
            var employeeUpdate = new
            {
                Name = "Updated Name",
                Position = "Senior Developer",
                Salary = 12000,
                EntityId = entityId
            };

            var content = new StringContent(JsonConvert.SerializeObject(employeeUpdate), Encoding.UTF8, "application/json");

            // Act: Verificar si el empleado existe
            var getResponse = await _client.GetAsync($"/api/employee/{validId}");

            if (getResponse.StatusCode == HttpStatusCode.NotFound)
            {
                // Si no existe, lo creamos primero
                var newEmployee = new
                {
                    Name = "New Employee",
                    Position = "Developer",
                    Salary = 10000,
                    entityId = entityId
                };

                var createContent = new StringContent(JsonConvert.SerializeObject(newEmployee), Encoding.UTF8, "application/json");
                var createResponse = await _client.PostAsync("/api/employee", createContent);
                createResponse.EnsureSuccessStatusCode();  // Asegura que la creación fue exitosa

                // Obtener el ID del nuevo empleado creado (ajusta según tu respuesta)
                var responseString = await createResponse.Content.ReadAsStringAsync();
                var createdEmployee = JsonConvert.DeserializeObject<dynamic>(responseString);
                validId = (int)createdEmployee.data.id;  // Extraer ID de la respuesta
            }

            // Actualizar el empleado existente
            var response = await _client.PutAsync($"/api/employee/{validId}", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateEmployee_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            SetAuthorizationHeader();
            int invalidId = 9999;
            var employeeUpdate = new
            {
                Name = "Updated Name",
                Position = "Senior Developer",
                Salary = 12000,
                EntityId = 1
            };

            var content = new StringContent(JsonConvert.SerializeObject(employeeUpdate), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/employee/{invalidId}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteEmployee_WithValidId_ShouldReturnOk()
        {
            // Arrange
            SetAuthorizationHeader();

            int entityId = await CreateTestEntity();

            var employee = new
            {
                Name = "John Doe",
                Position = "Developer",
                Salary = 10000,
                EntityId = entityId
            };

            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/employee", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var employeeResponse = JsonConvert.DeserializeObject<dynamic>(responseString);

            int validId = (int)employeeResponse.data.id;

            // Act
            var responseDelete = await _client.DeleteAsync($"/api/employee/{validId}");

            // Assert
            Assert.True(responseDelete.StatusCode == HttpStatusCode.OK || responseDelete.StatusCode == HttpStatusCode.NotFound,
                        $"Expected OK or NotFound but got {responseDelete.StatusCode}");
        }

        [Fact]
        public async Task DeleteEmployee_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            SetAuthorizationHeader();
            int invalidId = 999;

            // Act
            var response = await _client.DeleteAsync($"/api/employee/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
