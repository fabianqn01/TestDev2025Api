using Application.DTOs;

namespace Application.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetEmployeeAsync(int id);
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
        Task<RegistrationResponse> CreateEmployeeAsync(EmployeeDTO employeeDTO);
        Task<RegistrationResponse> UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO);
        Task<RegistrationResponse> DeleteEmployeeAsync(int id);
    }
}
