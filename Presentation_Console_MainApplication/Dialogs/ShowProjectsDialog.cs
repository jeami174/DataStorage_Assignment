using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs;

public static class ShowProjectsDialog
{
    public static void Show(IEnumerable<ProjectModel> projects)
    {
        Console.Clear();
        Console.WriteLine("------------- ALL PROJECTS -------------");

        if (projects.Any())
        {
            int index = 1;
            foreach (var project in projects)
            {
                Console.WriteLine($"{index++}. [{project.Id}] {project.Title.PadRight(20)} | {project.StartDate.ToShortDateString()} - {project.EndDate.ToShortDateString()} | {project.Status.StatusTypeName}");
            }
        }
        else
        {
            Console.WriteLine("No projects found.");
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }
}


