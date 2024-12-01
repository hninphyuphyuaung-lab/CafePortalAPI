using MediatR;
using CafePortalAPI.Application.Queries;
using CafePortalAPI.Application.Interfaces;
using CafePortalAPI.Application.Models;
using CafePortalAPI.Application.Commands.Cafe;

namespace CafePortalAPI.Application.Handlers
{
    public class CafeHandler :
        IRequestHandler<CreateCafeCommand, CafeDto>,
        IRequestHandler<GetCafesQuery, IEnumerable<CafeDto>>,
        IRequestHandler<UpdateCafeCommand, CafeDto>,
        IRequestHandler<DeleteCafeCommand>
    {
        private readonly ICafeService _cafeService;

        public CafeHandler(ICafeService cafeService)
        {
            _cafeService = cafeService;
        }

        // Handle CreateCafeCommand
        public async Task<CafeDto> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            return await _cafeService.CreateCafe(request);
        }

        // Handle GetCafesQuery
        public async Task<IEnumerable<CafeDto>> Handle(GetCafesQuery request, CancellationToken cancellationToken)
        {
            var cafes = await _cafeService.GetAllCafes();
            
            // If a location is provided, filter cafes by location
            if (!string.IsNullOrEmpty(request.Location))
            {
                cafes = cafes.Where(c => c.Location.Equals(request.Location, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Sort cafes by the number of employees, highest first
            return cafes.OrderByDescending(c => c.TotalEmployee);
        }

        public async Task<CafeDto> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            return await _cafeService.UpdateCafe(request);           
        }

        public async Task Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            await _cafeService.DeleteCafe(request.Id);
        }
    }
}
