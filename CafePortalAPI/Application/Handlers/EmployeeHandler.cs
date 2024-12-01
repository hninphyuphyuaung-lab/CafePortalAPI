using MediatR;
using CafePortalAPI.Application.Queries;
using CafePortalAPI.Application.Interfaces;
using CafePortalAPI.Application.Models;
using CafePortalAPI.Application.Commands.Employee;

namespace CafePortalAPI.Application.Handlers
{
    public class EmployeeHandler :
        IRequestHandler<CreateEmployeeCommand, EmployeeDto>,
        IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeDto>>,
        IRequestHandler<UpdateEmployeeCommand, EmployeeDto>,
        IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Handle CreateEmployeeCommand
        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _employeeService.CreateEmployee(request);
        }

        // Handle GetEMployeeQuery
        public async Task<IEnumerable<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAllEmployees();
            
            // If a cafe is provided, filter employees by cafe
            if (!string.IsNullOrEmpty(request.Cafe))
            {
                employees = employees.Where(c => !String.IsNullOrEmpty(c.Cafe) && c.Cafe.Equals(request.Cafe, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Sort employees by the highest number of working days 
            return employees.OrderByDescending(c => c.DaysWorked);
        }

        public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _employeeService.UpdateEmployee(request);
        }
        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeService.DeleteEmployee(request.Id);
        }
    }
}
