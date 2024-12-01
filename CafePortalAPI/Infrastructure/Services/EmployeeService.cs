using CafePortalAPI.Application.Interfaces;
using CafePortalAPI.Application.Models;
using CafePortalAPI.Domain;
using CafePortalAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using CafePortalAPI.Application.Commands.Employee;
using CafePortalAPI.Application.Commands.Cafe;
using CafePortalAPI.Helper;


namespace CafePortalAPI.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

    
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return employees.Select(c => new EmployeeDto
            {
                Id = c.Id,
                Name = c.Name,
                EmailAddress = c.EmailAddress,
                PhoneNumber = c.PhoneNumber,
                DaysWorked = (int)((DateTime.Now - c.StartDate).TotalDays),
                Cafe = c.Cafe?.Name,
                Gender = c.Gender
            });
        }

        public async Task<EmployeeDto> CreateEmployee(CreateEmployeeCommand command)
        {
            var employee = new Employee
            {
                Id = GenerateId(),
                Name = command.Name,
                EmailAddress = command.EmailAddress,
                Gender = Enum.GetName(typeof(Gender), command.Gender),
                PhoneNumber = command.PhoneNumber,
                StartDate = command.StartDate,
                CafeId = command.Cafe?.Id,
            };

            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);

            return new EmployeeDto
            {
                Id = createdEmployee.Id,
                Name = createdEmployee.Name,
                EmailAddress = createdEmployee.EmailAddress,
                Gender = createdEmployee.Gender,
                PhoneNumber = createdEmployee.PhoneNumber,
                DaysWorked = (int)((DateTime.Now - createdEmployee.StartDate).TotalDays),
                Cafe = command.Cafe?.Name
            };
        }

        public async Task<EmployeeDto> UpdateEmployee(UpdateEmployeeCommand command)
        {
            var employee = new Employee
            {
                Id = command.Id,
                Name = command.Name,
                EmailAddress = command.EmailAddress,
                Gender = Enum.GetName(typeof(Gender), command.Gender),
                PhoneNumber = command.PhoneNumber,
                StartDate = command.StartDate,
                CafeId = command.Cafe.Id,
            };

            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee);

            return new EmployeeDto
            {
                Id = updatedEmployee.Id,
                Name = updatedEmployee.Name,
                EmailAddress = updatedEmployee.EmailAddress,
                Gender = updatedEmployee.Gender,
                PhoneNumber = updatedEmployee.PhoneNumber,
                DaysWorked = (int)((DateTime.Now - updatedEmployee.StartDate).TotalDays),
                Cafe = command.Cafe?.Name
            };
        }

        public async Task DeleteEmployee(string id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
        }

        #region Genreate Random ID
        private string GenerateId()
        {
            string generatedId;
            var randomString = GenerateRandomString(8);
            generatedId = "UI" + randomString;

            // Ensure the ID does not exceed 10 characters
            if (generatedId.Length > 10)
            {
                generatedId = generatedId.Substring(0, 10); // Truncate to 10 characters s
            }
            return generatedId;
        }

        private string GenerateRandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // Alphanumeric characters
            var randomString = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomString[i] = chars[random.Next(chars.Length)];
            }

            return new string(randomString);
        }
    }
    #endregion
}

