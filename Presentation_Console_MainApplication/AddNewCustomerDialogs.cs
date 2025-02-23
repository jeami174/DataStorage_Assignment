using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Business.Dtos;
using Business.Models;
using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs;
    public static class AddNewCustomerDialog
    {
    /// <summary>
    /// Skapar en ny kund och låter användaren, om så önskas, koppla på en kontaktperson.
    /// Returnerar det nya kund-ID:t.
    /// </summary>
    public static async Task<int> CreateCustomerWithOptionalContactAsync(
        IList<CustomerModel> customers,
        ICustomerService customerService,
        ICustomerContactService customerContactService)
    {
        Console.Clear();
        Console.WriteLine("***** Create New Customer *****");
        Console.Write("Customer Name: ");
        string customerName = Console.ReadLine()!.Trim();
        var customerDto = new CustomerCreateDto { CustomerName = customerName };

        bool customerCreated = await customerService.CreateCustomerAsync(customerDto);
        if (customerCreated)
        {
            // Hämta en uppdaterad lista med kunder
            customers = (await customerService.GetAllCustomersAsync()).ToList();
            var newCustomer = customers.OrderByDescending(c => c.Id).First();
            Console.WriteLine($"New customer '{newCustomer.CustomerName}' created with ID {newCustomer.Id}.");

            // Fråga om man vill koppla en kontaktperson till kunden
            Console.Write("Do you want to attach a contact person to this customer? (y/n): ");
            var attachChoice = Console.ReadLine();
            if (attachChoice?.ToLower() == "y")
            {
                Console.WriteLine("Do you want to:");
                Console.WriteLine("1. Attach an existing contact person");
                Console.WriteLine("2. Create a new contact person");
                Console.Write("Enter your choice (1 or 2): ");
                var contactChoice = Console.ReadLine();

                if (contactChoice == "1")
                {
                    // Om kunden redan har några kontaktpersoner (sällan för en ny kund)
                    if (newCustomer.CustomerContacts != null && newCustomer.CustomerContacts.Any())
                    {
                        Console.WriteLine("Available contact persons for this customer:");
                        int index = 1;
                        foreach (var contact in newCustomer.CustomerContacts)
                        {
                            Console.WriteLine($"  {index++}. {contact.FirstName} {contact.LastName}, {contact.Email}");
                        }
                        Console.Write("Select contact (enter number): ");
                        if (!int.TryParse(Console.ReadLine(), out int contactIndex) ||
                            contactIndex < 1 || contactIndex > newCustomer.CustomerContacts.Count())
                        {
                            Console.WriteLine("Invalid input. No contact attached.");
                        }
                        else
                        {
                            Console.WriteLine("Contact attached to customer.");
                            // Här skulle du kunna spara kopplingen om det behövs (för en befintlig kontakt).
                        }
                    }
                    else
                    {
                        Console.WriteLine("No existing contacts found for this customer. Please create a new contact.");
                        await CreateNewCustomerContactDialogs.CreateNewContactAsync(newCustomer.Id, customerContactService);
                    }
                }
                else if (contactChoice == "2")
                {
                    await CreateNewCustomerContactDialogs.CreateNewContactAsync(newCustomer.Id, customerContactService);
                }
                else
                {
                    Console.WriteLine("Invalid choice. No contact attached.");
                }
            }
            return newCustomer.Id;
        }
        else
        {
            Console.WriteLine("Failed to create new customer. Defaulting to first available customer.");
            return customers.First().Id;
        }
    }
}

