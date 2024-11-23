using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebApi;
namespace WebApi.Test
{
    public class WebApiTestsBase
    {
        protected readonly HttpClient _client;

        public WebApiTestsBase()
        {
            var application = new WebApplicationFactory<Program>();
            _client = application.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetJwtToken());
        }

        public string GetJwtToken()
        {
            var loginData = new
            {
                Email = "user@example.com",
                Password = "string"
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");
            var response = _client.PostAsync("/api/user/login", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = response.Content.ReadAsStringAsync().Result;
                throw new Exception($"Failed to get JWT token. Status code: {response.StatusCode}, Response: {errorResponse}");
            }

            var responseString = response.Content.ReadAsStringAsync().Result;
            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseString);
            return tokenResponse.data.token;
        }
    }
}
