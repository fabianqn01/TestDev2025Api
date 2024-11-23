using Infrastructure.Data;
using Infrastructure.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.Tests.Repositories
{
    public class EntityRepositoryTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "EntityTestDb")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetEntityAsync_ShouldReturnEntity_WhenEntityExists()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EntityRepository(context);

            var entity = new Entity { Name = "Entity1", Description = "Test Entity" };
            context.Entities.Add(entity);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetEntityAsync(entity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Entity1", result.Name);
        }

        [Fact]
        public async Task GetAllEntitiesAsync_ShouldReturnAllEntities()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EntityRepository(context);

            context.Entities.AddRange(
                new Entity { Name = "Entity1", Description = "First Entity" },
                new Entity { Name = "Entity2", Description = "Second Entity" }
            );
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllEntitiesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateEntityAsync_ShouldAddEntityToDatabase()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EntityRepository(context);

            var newEntity = new Entity { Name = "New Entity", Description = "Test Description" };

            // Act
            var result = await repository.CreateEntityAsync(newEntity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Entity", result.Name);
            Assert.NotEqual(0, result.Id);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldUpdateEntityDetails()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EntityRepository(context);

            var entity = new Entity { Name = "Old Name", Description = "Old Description" };
            context.Entities.Add(entity);
            await context.SaveChangesAsync();

            var updatedEntity = new Entity { Name = "Updated Name", Description = "Updated Description" };

            // Act
            var result = await repository.UpdateEntityAsync(entity.Id, updatedEntity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
            Assert.Equal("Updated Description", result.Description);
        }

        [Fact]
        public async Task DeleteEntityAsync_ShouldRemoveEntityFromDatabase()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new EntityRepository(context);

            var entity = new Entity { Name = "Entity to Delete", Description = "Description" };
            context.Entities.Add(entity);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.DeleteEntityAsync(entity.Id);

            // Assert
            Assert.True(result);
            Assert.Null(await context.Entities.FindAsync(entity.Id));
        }
    }
}
