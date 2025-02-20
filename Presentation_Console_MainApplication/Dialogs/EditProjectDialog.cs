using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    /// <summary>
    /// Delegate för EditProjectDialog.
    /// Anropas när användaren har angett de uppdaterade projektdetaljerna.
    /// </summary>
    internal delegate Task EditProjectDialogDelegate(int id, ProjectUpdateDto dto);

    /// <summary>
    /// Konsoldialog för att redigera ett befintligt projekt (asynkront).
    /// </summary>
    internal static class EditProjectDialog
    {
        public static async Task ShowAsync(IList<ProjectModel> projects, IList<StatusTypeModel> statuses, EditProjectDialogDelegate callback)
        {
            Console.Clear();
            Console.WriteLine("------------- EDIT PROJECT -------------");

            // Använd en dialog för att välja vilket projekt som ska redigeras.
            // Vi utgår från att du har en SelectProjectDialog som returnerar index.
            int index = SelectProjectDialog.Show(projects);

            if (index != -1)
            {
                // Samla in de uppdaterade uppgifterna med hjälp av InputProjectUpdateDialog.
                var updateDto = InputProjectUpdateDialog.Show(projects[index], statuses);
                // Anropa callbacken asynkront med det valda projektets ID och de nya uppgifterna.
                await callback(projects[index].Id, updateDto);
            }

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
