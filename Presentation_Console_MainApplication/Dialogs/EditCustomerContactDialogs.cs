using System;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Interfaces;
using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class EditCustomerContactDialogs
    {
        /// <summary>
        /// Visar en dialog för att redigera en kundkontakt.
        /// </summary>
        public static async Task EditContactAsync(CustomerContactModel contact, ICustomerContactService customerContactService)
        {
            Console.Clear();
            Console.WriteLine("------------- EDIT CUSTOMER CONTACT -------------");
            Console.WriteLine($"Current First Name: {contact.FirstName}");
            Console.Write("New First Name (leave empty to keep current): ");
            string newFirstName = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(newFirstName))
                newFirstName = contact.FirstName;

            Console.WriteLine($"Current Last Name: {contact.LastName}");
            Console.Write("New Last Name (leave empty to keep current): ");
            string newLastName = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(newLastName))
                newLastName = contact.LastName;

            Console.WriteLine($"Current Email: {contact.Email}");
            Console.Write("New Email (leave empty to keep current): ");
            string newEmail = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(newEmail))
                newEmail = contact.Email;

            var updateDto = new CustomerContactUpdateDto
            {
                FirstName = newFirstName,
                LastName = newLastName,
                Email = newEmail
            };

            bool updated = await customerContactService.UpdateCustomerContactAsync(contact.Id, updateDto);
            if (updated)
                Console.WriteLine("Customer contact updated successfully.");
            else
                Console.WriteLine("Failed to update customer contact.");

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
