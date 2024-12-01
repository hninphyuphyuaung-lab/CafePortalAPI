using CafePortalAPI.Application.Models;
using MediatR;

namespace CafePortalAPI.Application.Queries
{
    public class GetEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>
    {
        public string? Cafe { get; set; }
        public GetEmployeesQuery(string? cafe)
        {
            Cafe = cafe;
        }
    }
}
