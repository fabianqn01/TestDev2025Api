using Domain.Entities;

namespace Application.Contracts
{
    public interface IEntityRepository
    {
        Task<Entity> GetEntityAsync(int id);
        Task<IEnumerable<Entity>> GetAllEntitiesAsync();
        Task<Entity> CreateEntityAsync(Entity entity); // Cambio aquí
        Task<Entity> UpdateEntityAsync(int id, Entity entity); // Cambio aquí
        Task<bool> DeleteEntityAsync(int id); // Debería retornar bool para saber si fue exitoso
    }
}
