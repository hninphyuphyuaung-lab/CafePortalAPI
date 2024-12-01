using CafePortalAPI.Application.Interfaces;
using CafePortalAPI.Domain;
using CafePortalAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CafePortalAPI.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        //Get all cafes including employees
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.Include(i => i.Cafe)
                .Where(e => e.CafeId == null || e.CafeId != null) 
                .ToListAsync(); ;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee request)
        {
            var employee = await _context.Employees.FindAsync(request.Id);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            employee.Name = request.Name;
            employee.EmailAddress = request.EmailAddress;
            employee.PhoneNumber = request.PhoneNumber;
            employee.Gender = request.Gender;
            employee.StartDate = request.StartDate;
            employee.CafeId = request.CafeId;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            _context.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIdExists(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee == null) return false;
            return true;
        }
    }
}
