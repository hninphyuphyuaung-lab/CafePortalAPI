using CafePortalAPI.Application.Models;
using MediatR;

namespace CafePortalAPI.Application.Queries
{
    public class GetCafesQuery : IRequest<IEnumerable<CafeDto>>
    {
        public string? Location { get; set; }
        public GetCafesQuery(string? location)
        {
            Location = location;
        }
    }
}
