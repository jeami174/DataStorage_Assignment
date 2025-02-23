using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs;

public static class DeleteProjectDialog
{
    public static async Task ShowAsync(IProjectService projectService)
    {
        var projects = (await projectService.GetAllProjectsWithDetailsAsync()).ToList();
        Console.Clear();
        Console.WriteLine("------------- DELETE PROJECT -------------");

        int index = SelectProjectDialog.Show(projects);
        if (index == -1)
        {
            Console.WriteLine("No valid project selected.");
            Console.Write("Press any key to return to the main menu...");
            Console.ReadKey();
            return;
        }

        var result = await projectService.DeleteProjectAsync(projects[index].Id);
        if (result)
            Console.WriteLine("Project deleted successfully!");
        else
            Console.WriteLine("Failed to delete project.");

        Console.Write("Press any key to return to the main menu...");
        Console.ReadKey();
    }
}