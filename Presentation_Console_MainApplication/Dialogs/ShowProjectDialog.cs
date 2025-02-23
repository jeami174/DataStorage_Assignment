using Business.Models;
using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs;

public static class ShowProjectDialog
{
    public static async Task ShowAsync(IProjectService projectService)
    {
        Console.Clear();
        Console.WriteLine("------------- ALL PROJECTS -------------");

        var projects = await projectService.GetAllProjectsWithDetailsAsync();

        if (projects.Any())
        {
            int index = 1;
            foreach (var project in projects)
            {
                Console.WriteLine($"{index++}. [{project.Id}] {project.Title.PadRight(20)} | {project.StartDate.ToShortDateString()} - {project.EndDate.ToShortDateString()} | {project.Status.StatusTypeName}");
            }

            Console.WriteLine("----------------------------------------");
            Console.Write("Enter the number of the project to view details or press Enter to return: ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= projects.Count())
            {
                var selectedProject = projects.ElementAt(selectedIndex - 1);
                ShowProjectDetails(selectedProject);
            }
            else
            {
                Console.WriteLine("Invalid selection. Returning to main menu.");
            }
        }
        else
        {
            Console.WriteLine("No projects found.");
        }

        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }

    private static void ShowProjectDetails(ProjectModel project)
    {
        Console.Clear();
        Console.WriteLine("--------- PROJECT DETAILS ---------");
        Console.WriteLine($"ID: {project.Id}");
        Console.WriteLine($"Title: {project.Title}");
        Console.WriteLine($"Description: {project.Description ?? "No description"}");
        Console.WriteLine($"Start Date: {project.StartDate:yyyy-MM-dd}");
        Console.WriteLine($"End Date: {project.EndDate:yyyy-MM-dd}");
        Console.WriteLine($"Status: {project.Status.StatusTypeName}");
        Console.WriteLine($"Customer: {project.Customer.CustomerName} ({project.Customer.CustomerContacts.First()?.ToString()})");
        Console.WriteLine($"Service: {project.Service.ServiceName} ({project.Service.Unit.UnitName}, {project.Service.PricePerUnit})");
        Console.WriteLine($"Quantity: {project.QuantityofServiceUnits}");
        Console.WriteLine($"Total Price: {project.TotalPrice}");
        Console.WriteLine($"Employee: {project.User.FirstName} {project.User.LastName} ({project.User.Role.RoleName})");
        Console.WriteLine("-----------------------------------");
    }
}
