using System;
using System.Threading.Tasks;
using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    /// <summary>
    /// Konsoldialog för att visa detaljer om ett enskilt projekt.
    /// </summary>
    public static class ShowProjectDialog
    {
        public static async Task ShowAsync(IList<ProjectModel> projects)
        {
            Console.Clear();
            Console.WriteLine("--------- SHOW PROJECT DETAILS ---------");

            // Använd en dialog för att välja vilket projekt som ska redigeras.
            // Vi utgår från att du har en SelectProjectDialog som returnerar index.
            int index = SelectProjectDialog.Show(projects);

            if (index >= 0)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"[{projects[index].Id}] {projects[index].Title}");
                Console.WriteLine($"Status: {projects[index].Status.StatusTypeName}");
                Console.WriteLine($"Beskrivning: {projects[index].Description ?? "Ingen beskrivning"}");
                Console.WriteLine($"Startdatum: {projects[index].StartDate:yyyy-MM-dd}");
                Console.WriteLine($"Slutdatum: {projects[index].EndDate:yyyy-MM-dd}");
                Console.WriteLine($"Kund: {projects[index].Customer.ToString()}");
                Console.WriteLine($"Service: {projects[index].Service.ToString()}");
                Console.WriteLine($"User: {projects[index].User.ToString()}");
                Console.WriteLine("----------------------------------------");
            }

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();

            await Task.CompletedTask;
        }
    }
}
