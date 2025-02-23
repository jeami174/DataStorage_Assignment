using System;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class EditUserRoleDialogs
    {
        /// <summary>
        /// Visar en dialog för att redigera en employee's role.
        /// Returnerar ett UserRoleUpdateDto med de nya värdena.
        /// </summary>
        public static async Task<UserRoleUpdateDto> ShowAsync(UserRoleModel role)
        {
            Console.Clear();
            Console.WriteLine("------------- EDIT EMPLOYEE ROLE -------------");
            Console.WriteLine($"Current Role Name: {role.RoleName}");
            Console.Write("New Role Name (leave empty to keep current): ");
            string input = Console.ReadLine()!;
            var updateDto = new UserRoleUpdateDto
            {
                RoleName = string.IsNullOrWhiteSpace(input) ? role.RoleName : input
            };
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            await Task.CompletedTask;
            return updateDto;
        }
    }
}
