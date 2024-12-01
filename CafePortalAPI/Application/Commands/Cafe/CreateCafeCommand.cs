using CafePortalAPI.Application.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CafePortalAPI.Application.Commands.Cafe
{
    public class CreateCafeCommand : IRequest<CafeDto>
    {
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Name must be between 6 and 100 characters.")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }
       
    }
}
