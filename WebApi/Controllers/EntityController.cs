using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly IEntityService _entityService;

        // Inyección de dependencias del servicio de Entidades
        public EntityController(IEntityService entityService)
        {
            _entityService = entityService;
        }

        // GET api/entity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntityDTO>>> GetAllEntities()
        {
            var entities = await _entityService.GetAllEntitiesAsync();
            return Ok(entities);
        }

        // GET api/entity/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EntityDTO>> GetEntityById(int id)
        {
            var entity = await _entityService.GetEntityAsync(id);  // Aquí usamos GetEntityAsync
            if (entity == null)
                return NotFound("Entity not found");

            return Ok(entity);
        }

        // POST api/entity (solo accesible para Administradores)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateEntity([FromBody] EntityDTO createEntityDTO)
        {
            var result = await _entityService.CreateEntityAsync(createEntityDTO);
            if (!result.Flag)
                return BadRequest(result.Message);

            return Ok(result);
                
        }

        // PUT api/entity/{id} (solo accesible para Administradores)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateEntity(int id, [FromBody] EntityDTO updateEntityDTO)
        {
            var result = await _entityService.UpdateEntityAsync(id, updateEntityDTO);
            if (!result.Flag)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // DELETE api/entity/{id} (solo accesible para Administradores)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteEntity(int id)
        {
            var result = await _entityService.DeleteEntityAsync(id);
            if (!result.Flag)
                return NotFound(result.Message);

            return Ok(result);
        }
    }
}
