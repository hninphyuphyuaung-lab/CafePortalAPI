using CafePortalAPI.Domain;

namespace CafePortalAPI.Application.Models
{
    public class CafeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int TotalEmployee { get; set; }
    }
}
