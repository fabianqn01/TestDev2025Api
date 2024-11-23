using Application.DTOs;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IEntityService
    {
        Task<EntityDTO> GetEntityAsync(int id);
        Task<IEnumerable<EntityDTO>> GetAllEntitiesAsync();
        Task<RegistrationResponse<Entity>> CreateEntityAsync(EntityDTO entityDTO);
        Task<RegistrationResponse<Entity>> UpdateEntityAsync(int id, EntityDTO entityDTO);
        Task<RegistrationResponse<Entity>> DeleteEntityAsync(int id);
    }
}
