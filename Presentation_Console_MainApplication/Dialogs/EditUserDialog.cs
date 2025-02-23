using System;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Models;
using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class EditUserDialog
    {
        /// <summary>
        /// Visar en dialog för att redigera en employee's information.
        /// Om användaren vill redigera rollen (employee role) sker detta i samma flöde.
        /// Returnerar ett UserUpdateDto med de nya värdena.
        /// </summary>
        public static async Task<UserUpdateDto> ShowAsync(UserModel employee, IUserRoleService userRoleService)
        {
            Console.Clear();
            Console.WriteLine("------------- EDIT EMPLOYEE -------------");

            Console.WriteLine($"Current First Name: {employee.FirstName}");
            Console.Write("New First Name (leave empty to keep current): ");
            string newFirstName = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(newFirstName))
                newFirstName = employee.FirstName;

            Console.WriteLine($"Current Last Name: {employee.LastName}");
            Console.Write("New Last Name (leave empty to keep current): ");
            string newLastName = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(newLastName))
                newLastName = employee.LastName;

            Console.WriteLine($"Current Email: {employee.Email}");
            Console.Write("New Email (leave empty to keep current): ");
            string newEmail = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(newEmail))
                newEmail = employee.Email;

            var updateDto = new UserUpdateDto
            {
                FirstName = newFirstName,
                LastName = newLastName,
                Email = newEmail
            };

            // Fråga om användaren vill redigera employee role
            Console.Write("Do you want to edit the employee's role? (y/n): ");
            string input = Console.ReadLine()!;
            if (input.ToLower() == "y")
            {
                if (employee.Role != null)
                {
                    var roleUpdateDto = await EditUserRoleDialogs.ShowAsync(employee.Role);
                    // Anta att en uppdatering sker via userRoleService.UpdateAsync
                    bool roleUpdated = await userRoleService.UpdateAsync(employee.Role.Id, roleUpdateDto);
                    if (roleUpdated)
                        Console.WriteLine("Employee role updated successfully.");
                    else
                        Console.WriteLine("Failed to update employee role.");
                }
                else
                {
                    Console.WriteLine("No role is currently associated with this employee.");
                }
            }
            else
            {
                Console.WriteLine("Employee role remains unchanged.");
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey();
            return updateDto;
        }
    }
}

