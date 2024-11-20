using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> CreateEmployeeAsync(EmployeeDTO employeeDTO)
        {
            var employee = new Employee
            {
                Name = employeeDTO.Name,
                Position = employeeDTO.Position,
                Salary = employeeDTO.Salary,
                EntityId = employeeDTO.EntityId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return null;

            employee.Name = employeeDTO.Name;
            employee.Position = employeeDTO.Position;
            employee.Salary = employeeDTO.Salary;
            employee.EntityId = employeeDTO.EntityId;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
