using CafePortalAPI.Application.Models;
using MediatR;

namespace CafePortalAPI.Application.Commands.Employee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public string Id { get; set; }
        public DeleteEmployeeCommand(string id)
        {
           Id = id;
        }
    }
}
