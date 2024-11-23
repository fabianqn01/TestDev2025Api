using Infrastructure.Data;
using Infrastructure.Repositories;
using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;
using System.Collections.Generic;

namespace Infrastructure.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserTestDb")
                .Options;

            return new AppDbContext(options);
        }

        private IConfiguration GetConfiguration()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "SuperSecretTestKey"},
                {"Jwt:Issuer", "TestIssuer"},
                {"Jwt:Audience", "TestAudience"}
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        

        [Fact]
        public async Task LoginUserAsync_ShouldReturnError_WhenUserNotFound()
        {
            // Arrange
            using var context = GetDbContext();
            var apiResponseService = new ApiResponseService();
            var repository = new UserRepository(context, GetConfiguration(), apiResponseService);

            var loginDTO = new LoginDTO { Email = "notfound@example.com", Password = "Password123" };

            // Act
            var result = await repository.LoginUserAsync(loginDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("User not found", result.Message);
        }

        [Fact]
        public async Task LoginUserAsync_ShouldReturnError_WhenPasswordIsInvalid()
        {
            // Arrange
            using var context = GetDbContext();
            var apiResponseService = new ApiResponseService();
            var repository = new UserRepository(context, GetConfiguration(), apiResponseService);

            var user = new ApplicationUser
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("CorrectPassword")
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var loginDTO = new LoginDTO { Email = "test@example.com", Password = "WrongPassword" };

            // Act
            var result = await repository.LoginUserAsync(loginDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid credentials", result.Message);
        }

        
    }
}
