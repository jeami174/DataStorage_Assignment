using Business.Dtos;
using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class CreateNewCustomerContactDialogs
    {
        public static async Task CreateNewContactAsync(int customerId, ICustomerContactService customerContactService)
        {
            Console.Clear();
            Console.WriteLine("***** Create New Customer Contact *****");
            Console.Write("First Name: ");
            string firstName = Console.ReadLine()!.Trim();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine()!.Trim();
            Console.Write("Email: ");
            string email = Console.ReadLine()!.Trim();

            var contactDto = new CustomerContactCreateDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                CustomerId = customerId
            };

            bool contactCreated = await customerContactService.CreateCustomerContactAsync(contactDto);
            if (contactCreated)
                Console.WriteLine("Customer contact created successfully.");
            else
                Console.WriteLine("Failed to create customer contact.");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
