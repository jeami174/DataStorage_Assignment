using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Presentation_Console_MainApplication.Interfaces;
using Presentation_Console_MainApplication.Dialogs;

namespace Presentation_Console_MainApplication.Dialogs
{
    public class CreateProjectDialog(
        IProjectService projectService,
        ICustomerService customerService,
        IServiceService serviceService,
        IUserService userService,
        ICustomerContactService customerContactService,
        IUserRoleService userRoleService
    ) : ICreateProjectDialog
    {
        private readonly IProjectService _projectService = projectService;
        private readonly ICustomerService _customerService = customerService;
        private readonly IServiceService _serviceService = serviceService;
        private readonly IUserService _userService = userService;
        private readonly ICustomerContactService _customerContactService = customerContactService;
        private readonly IUserRoleService _userRoleService = userRoleService;

        public async Task CreateProjectAsync()
        {
            Console.Clear();
            var projectCreateDto = ProjectFactory.Create();

            Console.WriteLine("------------ ADD NEW PROJECT -----------");
            Console.WriteLine("");

            Console.Write("Project Title: ");
            projectCreateDto.Title = Console.ReadLine()!;

            Console.Write("Project Description (optional): ");
            projectCreateDto.Description = Console.ReadLine();

            Console.Write("Start date (yyyy-MM-dd): ");
            string input = Console.ReadLine()!;
            if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
                projectCreateDto.StartDate = startDate;
            else
                projectCreateDto.StartDate = DateTime.Now;

            Console.Write("End date (yyyy-MM-dd): ");
            input = Console.ReadLine()!;
            if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                projectCreateDto.EndDate = endDate;
            else
                projectCreateDto.EndDate = DateTime.Now.AddDays(1);

            // Serviceval hanteras via AddNewServiceDialogs
            var services = (await _serviceService.GetAllServicesAsync()).ToList();
            projectCreateDto.ServiceId = await AddNewServiceDialogs.GetServiceIdAsync(services, _serviceService);

            // Kundval – välj mellan befintlig eller skapa ny (med möjlighet att koppla en kontaktperson)
            var customers = (await _customerService.GetAllCustomersAsync()).ToList();
            Console.WriteLine("Do you want to:");
            Console.WriteLine("1. Add existing customer");
            Console.WriteLine("2. Create new customer (with optional contact)");
            Console.Write("Enter your choice (1 or 2): ");
            var customerChoice = Console.ReadLine();
            if (customerChoice == "1")
            {
                Console.WriteLine("Available customers:");
                for (int i = 0; i < customers.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {customers[i].CustomerName}");
                }
                Console.Write("Select customer (enter number): ");
                if (!int.TryParse(Console.ReadLine(), out int customerIndex) ||
                    customerIndex < 1 || customerIndex > customers.Count)
                {
                    Console.WriteLine("Invalid input, using customer #1");
                    projectCreateDto.CustomerId = customers[0].Id;
                }
                else
                {
                    projectCreateDto.CustomerId = customers[customerIndex - 1].Id;
                }
            }
            else if (customerChoice == "2")
            {
                projectCreateDto.CustomerId = await CreateNewCustomerDialogs.CreateCustomerWithOptionalContactAsync(customers, _customerService, _customerContactService);
            }
            else
            {
                Console.WriteLine("Invalid input, defaulting to existing customer.");
                projectCreateDto.CustomerId = customers[0].Id;
            }

            // Employee (User) val – välj mellan befintlig employee eller skapa en ny via CreateNewUserDialogs
            var employees = (await _userService.GetAllUsersAsync()).ToList();
            Console.WriteLine("Do you want to:");
            Console.WriteLine("1. Add existing employee");
            Console.WriteLine("2. Create new employee");
            Console.Write("Enter your choice (1 or 2): ");
            var employeeChoice = Console.ReadLine();
            if (employeeChoice == "1")
            {
                Console.WriteLine("Available employees:");
                for (int i = 0; i < employees.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {employees[i].FirstName} {employees[i].LastName}");
                }
                Console.Write("Select employee (enter number): ");
                if (!int.TryParse(Console.ReadLine(), out int employeeIndex) ||
                    employeeIndex < 1 || employeeIndex > employees.Count)
                {
                    Console.WriteLine("Invalid input, using employee #1");
                    projectCreateDto.UserId = employees[0].Id;
                }
                else
                {
                    projectCreateDto.UserId = employees[employeeIndex - 1].Id;
                }
            }
            else if (employeeChoice == "2")
            {
                projectCreateDto.UserId = await CreateNewUserDialogs.CreateEmployeeAsync(employees, _userService, _userRoleService);
            }
            else
            {
                Console.WriteLine("Invalid input, defaulting to existing employee.");
                projectCreateDto.UserId = employees[0].Id;
            }

            // Statusval – använd den nya dialogen för att välja en statustyp
            projectCreateDto.StatusId = await CreateNewStatusTypeDialog.GetStatusTypeIdAsync();

            // Skicka projekt-DTO:t till projektservicen
            bool created = await _projectService.CreateProjectAsync(projectCreateDto);
            if (created)
                Console.WriteLine("Project created successfully!");
            else
                Console.WriteLine("Failed to create project.");

            Console.Write("Press any key to return to main menu...");
            Console.ReadKey();
        }
    }
}




