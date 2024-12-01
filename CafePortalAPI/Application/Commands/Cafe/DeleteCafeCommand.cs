using MediatR;

namespace CafePortalAPI.Application.Commands.Cafe
{
    public class DeleteCafeCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteCafeCommand(Guid id)
        {
           Id = id;
        }
    }
}
