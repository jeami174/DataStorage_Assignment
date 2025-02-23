using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    /// <summary>
    /// Konsoldialog för att välja ett projekt från en lista.
    /// </summary>
    internal class SelectUserDialog
    {
        public static int Show(IList<UserModel> employees)
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees available.");
                return -1;
            }

            Console.WriteLine("Available employees:");
            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {employees[i].FirstName} {employees[i].LastName}");
            }
            Console.Write("Select employee (enter number): ");
            if (!int.TryParse(Console.ReadLine(), out int employeeIndex) ||
                employeeIndex < 1 || employeeIndex > employees.Count)
            {
                Console.WriteLine("Invalid input, using employee #1");
                return employees[0].Id;
            }
            else
            {
                return employees[employeeIndex - 1].Id;
            }
        }
    }
}
