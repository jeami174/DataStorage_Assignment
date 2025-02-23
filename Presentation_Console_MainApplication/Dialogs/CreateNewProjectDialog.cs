using System.Globalization;
using Business.Dtos;
using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class CreateNewProjectDialog
    {
        public static async Task CreateProjectAsync(IProjectService projectService,
                ICustomerService customerService,
                IServiceService serviceService,
                IUserService userService,
                ICustomerContactService customerContactService,
                IUserRoleService userRoleService,
                IUnitService unitService,
                IStatusTypeService statusTypeService)
        {
            Console.Clear();
            var projectCreateDto = new ProjectCreateDto();

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

            // Serviceval - välj mellan befintlig eller skapa ny (med möjlighet att ange pris och enhet)
            var services = (await serviceService.GetAllServicesAsync()).ToList();
            if (services.Count == 0)
            {
                Console.WriteLine("No services available. Please add a service first.");
                projectCreateDto.ServiceId = await CreateNewServiceDialog.GetServiceIdAsync(serviceService, unitService);
            }
            else
            {
                Console.WriteLine("Do you want to:");
                Console.WriteLine("1. Add existing service");
                Console.WriteLine("2. Create new service");
                Console.Write("Enter your choice (1 or 2): ");
                var serviceChoice = Console.ReadLine();
                if (serviceChoice == "1")
                {
                    projectCreateDto.ServiceId = SelectServiceDialog.Show(services);
                }
                else if (serviceChoice == "2")
                {
                    projectCreateDto.ServiceId = await CreateNewServiceDialog.GetServiceIdAsync(serviceService, unitService);
                }
                else
                {
                    Console.WriteLine("Invalid input, defaulting to existing service.");
                    projectCreateDto.ServiceId = services[0].Id;
                }
            }

            // Antal enheter av service
            Console.Write("Quantity of service (enter number): ");
            if (!int.TryParse(Console.ReadLine(), out int serviceQuantity) ||
                serviceQuantity <= 0)
            {
                Console.WriteLine("Invalid input, using quantity 1.");
                projectCreateDto.QuantityofServiceUnits = 1;
            }
            else
            {
                projectCreateDto.QuantityofServiceUnits = serviceQuantity;
            }

            // Kundval – välj mellan befintlig eller skapa ny (med möjlighet att koppla en kontaktperson)
            var customers = (await customerService.GetAllCustomersAsync()).ToList();
            if (customers.Count == 0)
            {
                Console.WriteLine("No customers available. Please add a customer first.");
                projectCreateDto.CustomerId = await CreateNewCustomerDialogs.CreateCustomerWithOptionalContactAsync(customerService, customerContactService);
            }
            else
            {
                Console.WriteLine("Do you want to:");
                Console.WriteLine("1. Add existing customer");
                Console.WriteLine("2. Create new customer (with optional contact)");
                Console.Write("Enter your choice (1 or 2): ");
                var customerChoice = Console.ReadLine();
                if (customerChoice == "1")
                {
                    projectCreateDto.CustomerId = SelectCustomerDialog.Show(customers);
                }
                else if (customerChoice == "2")
                {
                    projectCreateDto.CustomerId = await CreateNewCustomerDialogs.CreateCustomerWithOptionalContactAsync(customerService, customerContactService);
                }
                else
                {
                    Console.WriteLine("Invalid input, defaulting to existing customer.");
                    projectCreateDto.CustomerId = customers[0].Id;
                }
            }

            // Employee (User) val – välj mellan befintlig employee eller skapa en ny via CreateNewUserDialogs
            var employees = (await userService.GetAllUsersAsync()).ToList();
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees available. Please add an employee first.");
                projectCreateDto.UserId = await CreateNewUserDialogs.CreateEmployeeAsync(employees, userService, userRoleService);
            }
            else
            {
                Console.WriteLine("Do you want to:");
                Console.WriteLine("1. Add existing employee");
                Console.WriteLine("2. Create new employee");
                Console.Write("Enter your choice (1 or 2): ");
                var employeeChoice = Console.ReadLine();
                if (employeeChoice == "1")
                {
                    projectCreateDto.UserId = SelectUserDialog.Show(employees);
                }
                else if (employeeChoice == "2")
                {
                    projectCreateDto.UserId = await CreateNewUserDialogs.CreateEmployeeAsync(employees, userService, userRoleService);
                }
                else
                {
                    Console.WriteLine("Invalid input, defaulting to existing employee.");
                    projectCreateDto.UserId = employees[0].Id;
                }
            }

            // Statusval – använd den nya dialogen för att välja en statustyp
            var statusTypes = (await statusTypeService.GetAllStatusTypesAsync()).ToList();
            projectCreateDto.StatusId = SelectStatusTypeDialog.Show(statusTypes);

            // Skicka projekt-DTO:t till projektservicen
            bool created = await projectService.CreateProjectAsync(projectCreateDto);
            if (created)
                Console.WriteLine("Project created successfully!");
            else
                Console.WriteLine("Failed to create project.");

            Console.Write("Press any key to return to main menu...");
            Console.ReadKey();
        }
    }
}
