using Application.DTOs;

namespace Application.Contracts
{
    public interface IEntityService
    {
        Task<EntityDTO> GetEntityAsync(int id);
        Task<IEnumerable<EntityDTO>> GetAllEntitiesAsync();
        Task<RegistrationResponse> CreateEntityAsync(EntityDTO entityDTO);
        Task<RegistrationResponse> UpdateEntityAsync(int id, EntityDTO entityDTO);
        Task<RegistrationResponse> DeleteEntityAsync(int id);
    }
}
