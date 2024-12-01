using CafePortalAPI.Application.Commands.Employee;
using CafePortalAPI.Application.Models;

namespace CafePortalAPI.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto> CreateEmployee(CreateEmployeeCommand command);
        Task<EmployeeDto> UpdateEmployee(UpdateEmployeeCommand command);
        Task DeleteEmployee(string id);
    }
}
