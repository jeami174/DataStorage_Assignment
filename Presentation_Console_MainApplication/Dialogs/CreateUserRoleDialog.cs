using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Business.Dtos;
using Business.Models;
using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class CreateUserRoleDialogs
    {
        /// <summary>
        /// Hanterar rollvalet: välj en befintlig roll eller skapa en ny.
        /// Returnerar det valda roll-ID:t.
        /// </summary>
        public static async Task<int> GetUserRoleIdAsync(IUserRoleService userRoleService)
        {
            var roles = (await userRoleService.GetAllUserRolesAsync()).ToList();
            Console.WriteLine("Role selection:");
            Console.WriteLine("Do you want to:");
            Console.WriteLine("1. Select existing role");
            Console.WriteLine("2. Create new role");
            Console.Write("Enter your choice (1 or 2): ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                if (roles.Count > 0)
                {
                    Console.WriteLine("Available roles:");
                    for (int i = 0; i < roles.Count; i++)
                    {
                        Console.WriteLine($"  {i + 1}. {roles[i].RoleName}");
                    }
                    Console.Write("Select role (enter number): ");
                    if (!int.TryParse(Console.ReadLine(), out int roleIndex) || roleIndex < 1 || roleIndex > roles.Count)
                    {
                        Console.WriteLine("Invalid input, using role #1");
                        return roles[0].Id;
                    }
                    else
                    {
                        return roles[roleIndex - 1].Id;
                    }
                }
                else
                {
                    Console.WriteLine("No roles available. Creating a new role.");
                    return await CreateNewRoleAsync(userRoleService);
                }
            }
            else if (choice == "2")
            {
                return await CreateNewRoleAsync(userRoleService);
            }
            else
            {
                Console.WriteLine("Invalid input, defaulting to existing role if available.");
                return roles.Count > 0 ? roles[0].Id : await CreateNewRoleAsync(userRoleService);
            }
        }

        private static async Task<int> CreateNewRoleAsync(IUserRoleService userRoleService)
        {
            Console.Clear();
            Console.WriteLine("***** Create New Role *****");
            Console.Write("Role Name: ");
            string roleName = Console.ReadLine()!.Trim();
            var roleDto = new UserRoleCreateDto { RoleName = roleName };

            bool roleCreated = await userRoleService.CreateAsync(roleDto);
            if (roleCreated)
            {
                var roles = (await userRoleService.GetAllUserRolesAsync()).ToList();
                var newRole = roles.OrderByDescending(r => r.Id).First();
                Console.WriteLine($"New role '{newRole.RoleName}' created with ID {newRole.Id}.");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
                return newRole.Id;
            }
            else
            {
                Console.WriteLine("Failed to create new role, defaulting to role #1 if available.");
                var roles = (await userRoleService.GetAllUserRolesAsync()).ToList();
                return roles.Count > 0 ? roles[0].Id : 1;
            }
        }
    }
}
