namespace CafePortalAPI.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CafePortalAPI.Domain;
    public interface ICafeRepository
    {
        Task<IEnumerable<Cafe>> GetAllCafesAsync();
        Task<Cafe> CreateCafeAsync(Cafe cafe);
        Task<Cafe> UpdateCafeAsync(Cafe cafe);
        Task DeleteCafeAsync(Guid id);
    }
}


