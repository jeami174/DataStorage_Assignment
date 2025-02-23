using Business.Dtos;
using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs;

public static class CreateNewCustomerDialogs
{
    public static async Task<int> CreateCustomerWithOptionalContactAsync(
        ICustomerService customerService,
        ICustomerContactService customerContactService)
    {
        Console.Clear();
        Console.WriteLine("----- Create New Customer -----");
        Console.Write("Customer Name: ");
        string customerName = Console.ReadLine()!.Trim();
        var customerDto = new CustomerCreateDto { CustomerName = customerName };

        bool customerCreated = await customerService.CreateCustomerAsync(customerDto);
        var customers = (await customerService.GetAllCustomersAsync()).ToList();
        if (customerCreated)
        {
            var newCustomer = customers.OrderByDescending(c => c.Id).First();
            Console.WriteLine($"New customer '{newCustomer.CustomerName}' created with ID {newCustomer.Id}.");

            Console.Write("Do you want to attach a contact person? (y/n): ");
            var input = Console.ReadLine()!;
            if (input.ToLower() == "y")
            {
                await CreateNewCustomerContactDialogs.CreateNewContactAsync(newCustomer.Id, customerContactService);
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
