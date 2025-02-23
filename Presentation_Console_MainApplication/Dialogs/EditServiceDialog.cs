using System;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class EditServiceDialog
    {
        /// <summary>
        /// Visar en dialog för att redigera en befintlig service.
        /// Returnerar ett ServiceUpdateDto med de nya värdena.
        /// </summary>
        public static async Task<ServiceUpdateDto> ShowAsync(ServiceModel service)
        {
            Console.Clear();
            Console.WriteLine("***** EDIT SERVICE *****");

            // Redigera servicens namn
            Console.WriteLine($"Current Service Name: {service.ServiceName}");
            Console.Write("New Service Name (leave empty to keep current): ");
            string newName = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(newName))
                newName = service.ServiceName;

            // Redigera pris per enhet
            Console.WriteLine($"Current Price per Unit: {service.PricePerUnit}");
            Console.Write("New Price per Unit (leave empty to keep current): ");
            string input = Console.ReadLine()!;
            decimal newPrice;
            if (!string.IsNullOrWhiteSpace(input) && decimal.TryParse(input, out newPrice))
            {
                // nytt pris används
            }
            else
            {
                newPrice = service.PricePerUnit;
            }

            var updateDto = new ServiceUpdateDto
            {
                ServiceName = newName,
                PricePerUnit = newPrice
            };

            Console.Write("Press any key to continue...");
            Console.ReadKey();
            await Task.CompletedTask;
            return updateDto;
        }
    }
}

