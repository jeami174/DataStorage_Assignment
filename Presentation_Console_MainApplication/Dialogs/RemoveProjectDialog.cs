using System;
using System.Collections.Generic;
using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    /// <summary>
    /// Delegate för RemoveProjectDialog.
    /// Anropas när användaren har valt ett projekt att ta bort.
    /// </summary>
    internal delegate void RemoveProjectDialogDelegate(int id);

    /// <summary>
    /// Konsoldialog för att ta bort ett projekt.
    /// </summary>
    internal class RemoveProjectDialog
    {
        public static void Show(IList<ProjectModel> projects, RemoveProjectDialogDelegate callback)
        {
            Console.Clear();
            Console.WriteLine("----- Remove a Project -----");

            int index = SelectProjectDialog.Show(projects);

            if (index != -1)
            {
                callback(projects[index].Id);
            }

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
