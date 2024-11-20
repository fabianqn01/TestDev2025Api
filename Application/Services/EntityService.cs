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
                throw new ArgumentException("Entity not found");
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

        public async Task<RegistrationResponse> CreateEntityAsync(EntityDTO entityDTO)
        {
            var entity = new Entity
            {
                Name = entityDTO.Name,
                Description = entityDTO.Description
            };

            var createdEntity = await _entityRepository.CreateEntityAsync(entity);

            return new RegistrationResponse(true, "Entity created successfully");
        }

        public async Task<RegistrationResponse> UpdateEntityAsync(int id, EntityDTO entityDTO)
        {
            var entity = await _entityRepository.GetEntityAsync(id);
            if (entity == null)
            {
                return new RegistrationResponse(false, "Entity not found");
            }

            entity.Name = entityDTO.Name;
            entity.Description = entityDTO.Description;

            await _entityRepository.UpdateEntityAsync(id, entity);

            return new RegistrationResponse(true, "Entity updated successfully");
        }

        public async Task<RegistrationResponse> DeleteEntityAsync(int id)
        {
            var entity = await _entityRepository.GetEntityAsync(id);
            if (entity == null)
            {
                return new RegistrationResponse(false, "Entity not found");
            }

            var isDeleted = await _entityRepository.DeleteEntityAsync(id);

            return new RegistrationResponse(isDeleted, isDeleted ? "Entity deleted successfully" : "Failed to delete entity");
        }
    }
}
