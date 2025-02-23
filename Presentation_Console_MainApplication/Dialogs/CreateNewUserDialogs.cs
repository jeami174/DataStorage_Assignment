using Business.Interfaces;
using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs;

public static class CreateNewUserDialogs
{
    public static async Task<int> CreateEmployeeAsync(
        IList<UserModel> employees,
        IUserService userService,
        IUserRoleService userRoleService)
    {
        Console.Clear();
        Console.WriteLine("***** Create New Employee *****");
        Console.Write("First Name: ");
        string firstName = Console.ReadLine()!.Trim();
        Console.Write("Last Name: ");
        string lastName = Console.ReadLine()!.Trim();
        Console.Write("Email: ");
        string email = Console.ReadLine()!.Trim();

        int roleId = await CreateUserRoleDialogs.GetUserRoleIdAsync(userRoleService);

        var userDto = new UserCreateDto
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            RoleId = roleId
        };

        bool created = await userService.CreateUserAsync(userDto);
        if (created)
        {
            employees = (await userService.GetAllUsersAsync()).ToList();
            var newEmployee = employees.OrderByDescending(u => u.Id).First();
            Console.WriteLine($"New employee '{newEmployee.FirstName} {newEmployee.LastName}' created with ID {newEmployee.Id}.");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            return newEmployee.Id;
        }
        else
        {
            Console.WriteLine("Failed to create new employee, defaulting to existing employee.");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            return employees[0].Id;
        }
    }
}
