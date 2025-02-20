using System;
using System.Globalization;
using Business.Dtos;
using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    internal class InputProjectDialog
    {
        public static ProjectCreateDto Show(IList<CustomerModel> customers, IList<ServiceModel> services, IList<UserModel> users)
        {
            var dto = new ProjectCreateDto();

            Console.Write("Title: ");
            dto.Title = Console.ReadLine()!;

            Console.Write("Description (optional): ");
            dto.Description = Console.ReadLine();

            Console.Write("Start date (yyyy-MM-dd): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
                dto.StartDate = startDate;
            else
                dto.StartDate = DateTime.Now;

            Console.Write("End date (yyyy-MM-dd): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                dto.EndDate = endDate;
            else
                dto.EndDate = DateTime.Now.AddDays(1);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Available customers: ");
            for (int i = 0; i< customers.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {customers[i]}");
            }
            Console.Write("Select customer: ");
            if (!int.TryParse(Console.ReadLine(), out int customerIndex) || customerIndex < 1 || customerIndex > customers.Count)
            {
                Console.WriteLine("Invalid input, using customer #1");
                dto.CustomerId = customers[0].Id;
            }
            else
            {
                dto.CustomerId = customers[customerIndex - 1].Id;
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Available services: ");
            for (int i = 0; i < services.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {services[i]}");
            }
            Console.Write("Select service: ");
            if (!int.TryParse(Console.ReadLine(), out int serviceIndex) || serviceIndex < 1 || serviceIndex > services.Count)
            {
                Console.WriteLine("Invalid input, using service #1");
                dto.ServiceId = services[0].Id; ;
            }
            else
            {
                dto.ServiceId = services[serviceIndex - 1].Id;
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Available users: ");
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {users[i]}");
            }
            Console.Write("Select user: ");
            if (!int.TryParse(Console.ReadLine(), out int userIndex) || userIndex < 1 || userIndex > users.Count)
            {
                Console.WriteLine("Invalid input, using user #1");
                dto.UserId = users[0].Id;
            }
            else
            {
                dto.UserId = users[userIndex - 1].Id;
            }

            // Nyskapade har alltid samma status.
            dto.StatusId = 1;

            return dto;
        }
    }
}
