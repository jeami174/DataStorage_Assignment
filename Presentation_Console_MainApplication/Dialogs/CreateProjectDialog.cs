using System;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    internal delegate Task CreateProjectDialogDelegate(ProjectCreateDto dto);
    internal static class CreateProjectDialog
    {
        public static async Task ShowAsync(IList<CustomerModel> customers, IList<ServiceModel> services, IList<UserModel> users, CreateProjectDialogDelegate callback)
        {
            Console.Clear();
            Console.WriteLine("------------ ADD NEW PROJECT -----------");

            var dto = InputProjectDialog.Show(customers, services, users);

            await callback(dto);

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
