using Application.DTOs;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetEmployeeAsync(int id);
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
        Task<RegistrationResponse<Employee>> CreateEmployeeAsync(EmployeeDTO employeeDTO);
        Task<RegistrationResponse<Employee>> UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO);
        Task<RegistrationResponse<Employee>> DeleteEmployeeAsync(int id);
    }
}
