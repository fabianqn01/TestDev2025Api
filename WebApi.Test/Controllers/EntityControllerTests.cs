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
    public class EntityControllerTests : WebApiTestsBase
    {
        // Configura la autorización para pruebas protegidas
        private void SetAuthorizationHeader()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetJwtToken());
        }

        [Fact]
        public async Task GetAllEntities_ShouldReturnSuccessStatusCode()
        {
            // Arrange
            SetAuthorizationHeader();

            // Act
            var response = await _client.GetAsync("/api/entity");

            // Assert
            Assert.True(response.IsSuccessStatusCode, $"Expected success status code but got {response.StatusCode}");
        }

        [Fact]
        public async Task GetEntityById_WithValidId_ShouldReturnSuccessStatusCode()
        {
            // Arrange
            SetAuthorizationHeader();
            int validId = await CreateTestEntity();  // Create a test entity and get its ID

            // Act
            var response = await _client.GetAsync($"/api/entity/{validId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetEntityById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            SetAuthorizationHeader();
            int invalidId = 9999;

            // Act
            var response = await _client.GetAsync($"/api/entity/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateEntity_WithValidData_ShouldReturnOk()
        {
            // Arrange
            SetAuthorizationHeader();

            var entity = new
            {
                Name = "Test Entity",
                Description = "Test Description Entity"
            };

            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/entity", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateEntity_WithInvalidData_ShouldReturnBadRequest()
        {
            // Arrange
            SetAuthorizationHeader();

            var entity = new
            {
                // Missing required 'Name' field
                Description = "Test Description Entity"
            };

            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/entity", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateEntity_WithValidData_ShouldReturnOk()
        {
            // Arrange
            SetAuthorizationHeader();

            int validId = await CreateTestEntity();  // Create and get valid ID

            var entityUpdate = new
            {
                Name = "Updated Entity",
                Description = "Updated Description"
            };

            var content = new StringContent(JsonConvert.SerializeObject(entityUpdate), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/entity/{validId}", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateEntity_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            SetAuthorizationHeader();

            int invalidId = 9999;

            var entityUpdate = new
            {
                Name = "Updated Entity",
                Description = "Updated Description"
            };

            var content = new StringContent(JsonConvert.SerializeObject(entityUpdate), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/entity/{invalidId}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteEntity_WithValidId_ShouldReturnOk()
        {
            // Arrange
            SetAuthorizationHeader();

            int validId = await CreateTestEntity();  // Create and get valid ID

            // Act
            var response = await _client.DeleteAsync($"/api/entity/{validId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteEntity_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            SetAuthorizationHeader();
            int invalidId = 9999;

            // Act
            var response = await _client.DeleteAsync($"/api/entity/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        // Helper method to create a test entity and return its ID
        private async Task<int> CreateTestEntity()
        {
            var entity = new
            {
                Name = "Test Entity",
                Description = "Test Description Entity"
            };

            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/entity", content);

            response.EnsureSuccessStatusCode();  // Throws an exception if not 2xx

            var responseString = await response.Content.ReadAsStringAsync();
            var entityResponse = JsonConvert.DeserializeObject<dynamic>(responseString);

            return (int)entityResponse.data.id;  // Extract ID from response
        }
    }
}