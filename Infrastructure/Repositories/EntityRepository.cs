using Application.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private readonly AppDbContext _context;

        public EntityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Entity> GetEntityAsync(int id)
        {
            return await _context.Entities.Include(e => e.Employees).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Entity>> GetAllEntitiesAsync()
        {
            return await _context.Entities.Include(e => e.Employees).ToListAsync();
        }

        public async Task<Entity> CreateEntityAsync(Entity entity)
        {
            _context.Entities.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Entity> UpdateEntityAsync(int id, Entity entity)
        {
            var existingEntity = await _context.Entities.FindAsync(id);
            if (existingEntity == null) return null;

            existingEntity.Name = entity.Name;
            existingEntity.Description = entity.Description;

            _context.Entities.Update(existingEntity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }

        public async Task<bool> DeleteEntityAsync(int id)
        {
            var entity = await _context.Entities.FindAsync(id);
            if (entity == null) return false;

            _context.Entities.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
