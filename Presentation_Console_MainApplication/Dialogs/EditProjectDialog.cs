using System.Globalization;
using Business.Factories;
using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs;

public static class EditProjectDialog
{
    public static async Task EditProjectAsync(
        IProjectService projectService,
        ICustomerService customerService,
        ICustomerContactService customerContactService,
        IServiceService serviceService,
        IUserService userService,
        IUserRoleService userRoleService,
        IStatusTypeService statusTypeService)
    {
        var projects = (await projectService.GetAllProjectsWithDetailsAsync()).ToList();

        Console.Clear();
        Console.WriteLine("------------- EDIT PROJECT -------------");

        int index = SelectProjectDialog.Show(projects);
        if (index == -1)
        {
            Console.WriteLine("No valid project selected.");
            Console.Write("Press any key to return to the main menu...");
            Console.ReadKey();
            return;
        }
        var project = projects[index];
        var updateDto = ProjectFactory.CreateUpdateDto(project);

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Current Title: {project.Title}");
        Console.Write("New Title (leave empty to keep current): ");
        string input = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(input))
            updateDto.Title = input;

        Console.WriteLine($"Current Description: {project.Description}");
        Console.Write("New Description (leave empty to keep current): ");
        input = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(input))
            updateDto.Description = input;

        Console.WriteLine($"Current Start Date: {project.StartDate:yyyy-MM-dd}");
        Console.Write("New Start Date (yyyy-MM-dd, leave empty to keep current): ");
        input = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(input) &&
            DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newStart))
            updateDto.StartDate = newStart;

        Console.WriteLine($"Current End Date: {project.EndDate:yyyy-MM-dd}");
        Console.Write("New End Date (yyyy-MM-dd, leave empty to keep current): ");
        input = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(input) &&
            DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newEnd))
            updateDto.EndDate = newEnd;

        Console.WriteLine($"Current Quantity: {project.QuantityofServiceUnits}");
        Console.Write("New Quantity (leave empty to keep current): ");
        input = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int qty))
            updateDto.QuantityofServiceUnits = qty;

        Console.Write("Do you want to update the project status? (y/n): ");
        input = Console.ReadLine()!;
        if (input.ToLower() == "y")
        {
            var statusTypes = (await statusTypeService.GetAllStatusTypesAsync()).ToList();
            updateDto.StatusId = SelectStatusTypeDialog.Show(statusTypes);
        }

        Console.Write("Press any key to save project changes...");
        Console.ReadKey();

        bool projectUpdated = await projectService.UpdateProjectAsync(project.Id, updateDto);
        if (projectUpdated)
            Console.WriteLine("Project updated successfully!");
        else
            Console.WriteLine("Failed to update project.");

        Console.Write("Press any key to return to the main menu...");
        Console.ReadKey();
    }
}




