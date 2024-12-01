using CafePortalAPI.Application.Interfaces;
using CafePortalAPI.Application.Models;
using CafePortalAPI.Domain;
using CafePortalAPI.Application.Commands.Cafe;


namespace CafePortalAPI.Infrastructure.Services
{
    public class CafeService : ICafeService
    {
        private readonly ICafeRepository _cafeRepository;

        public CafeService(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

    
        public async Task<IEnumerable<CafeDto>> GetAllCafes()
        {
            var cafes = await _cafeRepository.GetAllCafesAsync();
            return cafes.Select(c => new CafeDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Location = c.Location,
                TotalEmployee = c.Employees.Count()
            });
        }

        public async Task<CafeDto> CreateCafe(CreateCafeCommand command)
        {
            var cafe = new Cafe
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Description = command.Description,
                Location = command.Location
            };

            var createdCafe = await _cafeRepository.CreateCafeAsync(cafe);

            return new CafeDto
            {
                Id = createdCafe.Id,
                Name = createdCafe.Name,
                Description = createdCafe.Description,
                Location = createdCafe.Location
            };
        }

        public async Task<CafeDto> UpdateCafe(UpdateCafeCommand command)
        {
            var cafe = new Cafe
            {
                Id = command.Id,
                Name = command.Name,
                Description = command.Description,
                Location = command.Location
            };

            var updatedCafe = await _cafeRepository.UpdateCafeAsync(cafe);

            return new CafeDto
            {
                Id = updatedCafe.Id,
                Name = updatedCafe.Name,
                Description = updatedCafe.Description,
                Location = updatedCafe.Location
            };
        }

        public async Task DeleteCafe(Guid id)
        {
            await _cafeRepository.DeleteCafeAsync(id);
        }
    }
}
