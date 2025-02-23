using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    /// <summary>
    /// Konsoldialog för att välja ett projekt från en lista.
    /// </summary>
    internal class SelectCustomerDialog
    {
        public static int Show(IList<CustomerModel> customers)
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("No customers available.");
                return -1;
            }

            Console.WriteLine("Available customers:");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {customers[i].CustomerName}");
            }
            Console.Write("Select customer (enter number): ");
            if (!int.TryParse(Console.ReadLine(), out int customerIndex) ||
                customerIndex < 1 || customerIndex > customers.Count)
            {
                Console.WriteLine("Invalid input, using customer #1");
                return customers[0].Id;
            }
            else
            {
                return customers[customerIndex - 1].Id;
            }
        }
    }
}
