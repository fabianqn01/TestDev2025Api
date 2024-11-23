using Application.Common.Exceptions;
using Application.Contracts;
using Application.DTOs;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class EntityServiceTests
    {
        private readonly Mock<IEntityRepository> _mockRepo;
        private readonly EntityService _service;

        public EntityServiceTests()
        {
            _mockRepo = new Mock<IEntityRepository>();
            _service = new EntityService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetEntityAsync_ReturnsEntity_WhenEntityExists()
        {
            // Arrange
            var entityId = 1;
            var entity = new Entity { Id = entityId, Name = "Entity One", Description = "Test Entity" };
            _mockRepo.Setup(repo => repo.GetEntityAsync(entityId))
                     .ReturnsAsync(entity);

            // Act
            var result = await _service.GetEntityAsync(entityId);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Entity One");
            _mockRepo.Verify(repo => repo.GetEntityAsync(entityId), Times.Once);
        }

        [Fact]
        public async Task GetEntityAsync_ThrowsNotFoundException_WhenEntityDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetEntityAsync(It.IsAny<int>()))
                     .ReturnsAsync((Entity)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetEntityAsync(1));
        }

        [Fact]
        public async Task CreateEntityAsync_ReturnsSuccessResponse_WhenEntityIsCreated()
        {
            // Arrange
            var entityDTO = new EntityDTO { Name = "New Entity", Description = "Description" };
            var entity = new Entity { Id = 1, Name = entityDTO.Name, Description = entityDTO.Description };

            _mockRepo.Setup(repo => repo.CreateEntityAsync(It.IsAny<Entity>()))
                     .ReturnsAsync(entity);

            // Act
            var result = await _service.CreateEntityAsync(entityDTO);

            // Assert
            result.Should().NotBeNull();
            result.Flag.Should().BeTrue();
            _mockRepo.Verify(repo => repo.CreateEntityAsync(It.IsAny<Entity>()), Times.Once);
        }
    }
}
