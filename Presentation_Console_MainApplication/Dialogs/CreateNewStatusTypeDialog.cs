using System;
using System.Threading.Tasks;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class CreateNewStatusTypeDialog
    {
        /// <summary>
        /// Låter användaren välja en av de tre fasta statustyperna:
        /// 1. Not started, 2. Started, 3. Completed.
        /// Returnerar det valda status-ID:t.
        /// </summary>
        public static async Task<int> GetStatusTypeIdAsync()
        {
            Console.Clear();
            Console.WriteLine("Select project status:");
            Console.WriteLine("1. Not started");
            Console.WriteLine("2. Started");
            Console.WriteLine("3. Completed");
            Console.Write("Enter your choice (1, 2, or 3): ");
            string? input = Console.ReadLine();
            int choice;
            if (!int.TryParse(input, out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Invalid input. Defaulting to 'Not started' (1).");
                choice = 1;
            }
            // Om du använder fasta ID:n i databasen, se till att dessa värden stämmer
            // Här antar vi att 1 = Not started, 2 = Started, 3 = Completed.
            await Task.CompletedTask;
            return choice;
        }
    }
}
