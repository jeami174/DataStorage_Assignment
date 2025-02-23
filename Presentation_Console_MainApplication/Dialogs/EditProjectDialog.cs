using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Models;
using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class EditProjectDialog
    {
        public static async Task EditProjectAsync(
            IProjectService projectService,
            ICustomerService customerService,
            ICustomerContactService customerContactService,
            IServiceService serviceService,
            IUserService userService,
            IUserRoleService userRoleService,
            IStatusTypeService statusTypeService, // Lägg till statusTypeService
            IList<ProjectModel> projects)
        {
            Console.Clear();
            Console.WriteLine("------------- EDIT PROJECT -------------");

            // Välj projekt att redigera
            int index = SelectProjectDialog.Show(projects);
            if (index == -1)
            {
                Console.WriteLine("No valid project selected.");
                Console.Write("Press any key to return to the main menu...");
                Console.ReadKey();
                return;
            }
            var project = projects[index];
            var updateDto = new ProjectUpdateDto
            {
                Title = project.Title,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                QuantityofServiceUnits = project.QuantityofServiceUnits,
                StatusTypeId = project.Status.Id // Behåll nuvarande status som standard
            };

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

            // Fråga om användaren vill uppdatera projektets status
            Console.Write("Do you want to update the project status? (y/n): ");
            input = Console.ReadLine()!;
            if (input.ToLower() == "y")
            {
                // Hämta tillgängliga statusar från databasen
                var statuses = (await statusTypeService.GetAllStatusTypesAsync()).ToList();

                // Definiera de tre fasta valen
                var statusOptions = new Dictionary<int, string>
                {
                    { 1, "Not Started" },
                    { 2, "In Progress" },
                    { 3, "Completed" }
                };

                Console.WriteLine("Select new status:");
                foreach (var option in statusOptions)
                {
                    Console.WriteLine($"  {option.Key}. {option.Value}");
                }
                Console.Write("Enter the number corresponding to the desired status: ");
                input = Console.ReadLine()!;
                if (int.TryParse(input, out int statusChoice) && statusOptions.ContainsKey(statusChoice))
                {
                    // Hitta motsvarande StatusTypeId baserat på det valda namnet
                    var selectedStatusName = statusOptions[statusChoice];
                    var selectedStatus = statuses.FirstOrDefault(s => s.StatusTypeName.Equals(selectedStatusName, StringComparison.OrdinalIgnoreCase));
                    if (selectedStatus != null)
                    {
                        updateDto.StatusTypeId = selectedStatus.Id;
                    }
                    else
                    {
                        Console.WriteLine("Selected status not found in the database.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection. Keeping current status.");
                }
            }

            // Fortsätt med resten av redigeringsprocessen...

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
}




