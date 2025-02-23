using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs;


internal class SelectProjectDialog
{
    public static int Show(IList<ProjectModel> projects)
    {
        for (int i = 0; i < projects.Count; i++)
        {
            Console.WriteLine($"{i + 1}. [{projects[i].Id}] {projects[i].Title}");
        }
        
        Console.WriteLine("----------------------------------------");

        Console.Write("Enter project number: ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > projects.Count)
        {
            Console.WriteLine("Invalid input.");
            return -1;
        }
        return index - 1;
    }
}
