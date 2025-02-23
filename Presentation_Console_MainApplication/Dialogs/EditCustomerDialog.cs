using System;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Interfaces;
using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class EditCustomerDialog
    {
        /// <summary>
        /// Visar en dialog för att redigera en kunds information.
        /// Ger användaren möjlighet att även redigera den associerade kontaktpersonen.
        /// Returnerar ett CustomerUpdateDto med de nya värdena.
        /// </summary>
        public static async Task<CustomerUpdateDto> ShowAsync(CustomerModel customer, ICustomerContactService customerContactService)
        {
            Console.Clear();
            Console.WriteLine("------------- EDIT CUSTOMER -------------");
            Console.WriteLine($"Current Customer Name: {customer.CustomerName}");
            Console.Write("New Customer Name (leave empty to keep current): ");
            string input = Console.ReadLine()!;
            var updateDto = new CustomerUpdateDto
            {
                CustomerName = string.IsNullOrWhiteSpace(input) ? customer.CustomerName : input
            };

            Console.Write("Do you want to edit the customer contact person? (y/n): ");
            string contactChoice = Console.ReadLine()!;
            if (contactChoice.ToLower() == "y")
            {
                if (customer.CustomerContacts != null && customer.CustomerContacts.Any())
                {
                    // Om det finns flera kontaktpersoner kan du låta användaren välja, här väljer vi den första.
                    var contact = customer.CustomerContacts.First();
                    await EditCustomerContactDialogs.EditContactAsync(contact, customerContactService);
                }
                else
                {
                    Console.WriteLine("No contact person found for this customer.");
                }
            }
            else
            {
                Console.WriteLine("Customer contact remains unchanged.");
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey();
            return updateDto;
        }
    }
}
