using CafePortalAPI.Application.Commands.Cafe;
using CafePortalAPI.Application.Models;

namespace CafePortalAPI.Application.Interfaces
{
    public interface ICafeService
    {
        Task<IEnumerable<CafeDto>> GetAllCafes();
        Task<CafeDto> CreateCafe(CreateCafeCommand command);
        Task<CafeDto> UpdateCafe(UpdateCafeCommand command);
        Task DeleteCafe(Guid id);
    }
}
