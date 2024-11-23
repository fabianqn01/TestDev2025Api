using Infrastructure.DependencyInjection;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Infrastructure.Tests.DependencyInjection
{
    public class ServiceContainerTests
    {
        [Fact]
        public void InfrastructureService_Should_Register_Services()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
            {
                { "Jwt:Key", "TestKey" },
                { "Jwt:Issuer", "TestIssuer" },
                { "Jwt:Audience", "TestAudience" },
                { "ConnectionStrings:Default", "TestConnectionString" }
            }).Build();

            // Act
            serviceCollection.InfrastructureService(configuration);
            var provider = serviceCollection.BuildServiceProvider();

            // Assert
            Assert.NotNull(provider.GetService<AppDbContext>());
        }
    }
}
