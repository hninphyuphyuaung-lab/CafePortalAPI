using CafePortalAPI.Application.Models;
using CafePortalAPI.Helper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CafePortalAPI.Application.Commands.Employee
{
    public class UpdateEmployeeCommand : IRequest<EmployeeDto>
    {
        [Required]
        public string Id { get; set; }

        [Required] 
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "Phone number must br 8 digits.")]
        [RegularExpression(@"^(8|9)\d*$", ErrorMessage = "Phone number must start with 8 or 9.")]
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime StartDate { get; set; }
        public CafeDto? Cafe { get; set; }

    }
}
