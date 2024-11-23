using Application.Common.Exceptions;
using Application.Contracts;
using Application.DTOs;
using Domain.Entities;

namespace Application.Services
{
    public class EntityService : IEntityService
    {
        private readonly IEntityRepository _entityRepository;

        public EntityService(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<EntityDTO> GetEntityAsync(int id)
        {
            var entity = await _entityRepository.GetEntityAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Entity), id);
            }

            return new EntityDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        public async Task<IEnumerable<EntityDTO>> GetAllEntitiesAsync()
        {
            var entities = await _entityRepository.GetAllEntitiesAsync();
            return entities.Select(entity => new EntityDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            });
        }

        public async Task<RegistrationResponse<Entity>> CreateEntityAsync(EntityDTO entityDTO)
        {
            var entity = new Entity
            {
                Name = entityDTO.Name,
                Description = entityDTO.Description
            };

            var createdEntity = await _entityRepository.CreateEntityAsync(entity);

            return new RegistrationResponse<Entity>(true, "Entity created successfully",createdEntity);
        }

        public async Task<RegistrationResponse<Entity>> UpdateEntityAsync(int id, EntityDTO entityDTO)
        {
            var entity = await _entityRepository.GetEntityAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Entity), id);
            }

            entity.Name = entityDTO.Name;
            entity.Description = entityDTO.Description;

            var updateEntity = await _entityRepository.UpdateEntityAsync(id, entity);

            return new RegistrationResponse<Entity>(true, "Entity updated successfully", updateEntity);
        }

        public async Task<RegistrationResponse<Entity>> DeleteEntityAsync(int id)
        {
            var entity = await _entityRepository.GetEntityAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Entity), id);
            }

            var isDeleted = await _entityRepository.DeleteEntityAsync(id);

            return new RegistrationResponse<Entity>(isDeleted, isDeleted ? "Entity deleted successfully" : "Failed to delete entity");
        }
    }
}
