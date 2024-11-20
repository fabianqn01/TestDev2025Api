using Application.Contracts;
using Application.DTOs;
using Domain.Entities;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDTO> GetEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(id);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found");
            }

            return new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                Salary = employee.Salary,
                EntityId = employee.EntityId
            };
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return employees.Select(employee => new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                Salary = employee.Salary,
                EntityId = employee.EntityId
            });
        }

        public async Task<RegistrationResponse> CreateEmployeeAsync(EmployeeDTO employeeDTO)
        {
            var employee = new Employee
            {
                Name = employeeDTO.Name,
                Position = employeeDTO.Position,
                Salary = employeeDTO.Salary
            };

            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employeeDTO);

            return new RegistrationResponse(true, "Employee created successfully");
        }

        public async Task<RegistrationResponse> UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(id);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found");
            }

            employee.Name = employeeDTO.Name;
            employee.Position = employeeDTO.Position;
            employee.Salary = employeeDTO.Salary;

            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(id, employeeDTO);

            return new RegistrationResponse(true, "Employee updated successfully");
        }

        public async Task<RegistrationResponse> DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(id);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found");
            }

            var result = await _employeeRepository.DeleteEmployeeAsync(id);

            return new RegistrationResponse(result, result ? "Employee deleted successfully" : "Failed to delete employee");
        }
    }
}
