using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        // Inyección de dependencias del servicio de Empleados
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        // GET api/employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeAsync(id);
            if (employee == null)
                return NotFound("Employee not found");

            return Ok(employee);
        }

        // POST api/employee (solo accesible para Administradores)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateEmployee([FromBody] EmployeeDTO createEmployeeDTO)
        {
            var result = await _employeeService.CreateEmployeeAsync(createEmployeeDTO);
            if (!result.Flag)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // PUT api/employee/{id} (solo accesible para Administradores)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateEmployee(int id, [FromBody] EmployeeDTO updateEmployeeDTO)
        {
            var result = await _employeeService.UpdateEmployeeAsync(id, updateEmployeeDTO);
            if (!result.Flag)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // DELETE api/employee/{id} (solo accesible para Administradores)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (!result.Flag)
                return NotFound(result.Message);

            return Ok(result);
        }
    }
}
