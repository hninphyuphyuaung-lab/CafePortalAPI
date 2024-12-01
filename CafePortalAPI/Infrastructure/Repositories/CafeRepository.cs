using CafePortalAPI.Application.Interfaces;
using CafePortalAPI.Domain;
using CafePortalAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CafePortalAPI.Infrastructure.Repositories
{
    public class CafeRepository : ICafeRepository
    {
        private readonly AppDbContext _context;

        public CafeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cafe>> GetAllCafesAsync()
        {
            return await _context.Cafes.Include(c => c.Employees).ToListAsync();
        }

        public async Task<Cafe> GetCafeByIdAsync(Guid id)
        {
            return await _context.Cafes.FindAsync(id);
        }

        public async Task<Cafe> CreateCafeAsync(Cafe request)
        {
            _context.Cafes.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<Cafe> UpdateCafeAsync(Cafe request)
        {
            var cafe = await _context.Cafes.FindAsync(request.Id);
            if (cafe == null)
            {
                throw new ArgumentException("Cafe not found.");
            }

            cafe.Name = request.Name;
            cafe.Description = request.Description;
            cafe.Location = request.Location;

            _context.Cafes.Update(cafe);
            await _context.SaveChangesAsync();
            return cafe; 
        }

        public async Task DeleteCafeAsync(Guid id)
        {
            var cafe = await _context.Cafes.FindAsync(id);
            if (cafe == null)
            {
                throw new ArgumentException("Cafe not found.");
            }

             _context.Remove(cafe);
            await _context.SaveChangesAsync();
        }
    }
}
