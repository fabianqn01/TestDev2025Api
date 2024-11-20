using Application.DTOs;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeAsync(int id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> CreateEmployeeAsync(EmployeeDTO employeeDTO);
        Task<Employee> UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}

