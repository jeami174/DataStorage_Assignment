using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Business.Dtos;
using Business.Models;
using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class AddNewServiceDialogs
    {
        /// <summary>
        /// Hanterar valet av service för ett projekt: välj en befintlig eller skapa en ny.
        /// Returnerar det valda service-ID:t.
        /// </summary>
        public static async Task<int> GetServiceIdAsync(
            IList<ServiceModel> services,
            IServiceService serviceService)
        {
            Console.Clear();
            Console.WriteLine("------------ ADD A SERVICE TO PROJECT ------------");
            Console.WriteLine("");
            Console.WriteLine("Do you want to:");
            Console.WriteLine("1. Add existing service");
            Console.WriteLine("2. Create new service and add to project");
            Console.Write("Enter your choice (1 or 2): ");
            var serviceChoice = Console.ReadLine();

            if (serviceChoice == "1")
            {
                Console.WriteLine("Available services:");
                for (int i = 0; i < services.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {services[i].ServiceName}");
                }
                Console.Write("Select service (enter number): ");
                if (!int.TryParse(Console.ReadLine(), out int serviceIndex) ||
                    serviceIndex < 1 || serviceIndex > services.Count)
                {
                    Console.WriteLine("Invalid input, using service #1");
                    return services[0].Id;
                }
                else
                {
                    return services[serviceIndex - 1].Id;
                }
            }
            else if (serviceChoice == "2")
            {
                Console.Clear();
                Console.WriteLine("***** Create New Service *****");
                Console.Write("Service Name: ");
                string serviceName = Console.ReadLine()!.Trim();
                Console.Write("Price per unit: ");
                decimal pricePerUnit = 0;
                if (!decimal.TryParse(Console.ReadLine(), out pricePerUnit))
                    pricePerUnit = 1;
                Console.Write("Unit ID: ");
                int unitId = 0;
                if (!int.TryParse(Console.ReadLine(), out unitId))
                    unitId = 1;
                var serviceDto = new ServiceCreateDto
                {
                    ServiceName = serviceName,
                    PricePerUnit = pricePerUnit,
                    UnitId = unitId
                };

                bool serviceCreated = await serviceService.CreateAsync(serviceDto);
                if (serviceCreated)
                {
                    // Hämta uppdaterad lista – antag att ny service har högst ID
                    services = (await serviceService.GetAllServicesAsync()).ToList();
                    var newService = services.OrderByDescending(s => s.Id).First();
                    Console.WriteLine($"New service '{newService.ServiceName}' created with ID {newService.Id}.");
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    return newService.Id;
                }
                else
                {
                    Console.WriteLine("Failed to create new service, defaulting to existing service.");
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    return services[0].Id;
                }
            }
            else
            {
                Console.WriteLine("Invalid input, defaulting to existing service.");
                return services[0].Id;
            }
        }
    }
}

