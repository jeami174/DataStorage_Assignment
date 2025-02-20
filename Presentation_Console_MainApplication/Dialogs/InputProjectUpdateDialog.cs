using System;
using System.Globalization;
using Business.Dtos;
using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    internal class InputProjectUpdateDialog
    {
        public static ProjectUpdateDto Show(ProjectModel project, IList<StatusTypeModel> statuses)
        {
            var dto = new ProjectUpdateDto();

            Console.WriteLine("----------------------------------------");

            Console.WriteLine($"Old title: {project.Title}");
            Console.Write("New title: ");
            dto.Title = Console.ReadLine()!;
            if (dto.Title == "")
                dto.Title = project.Title;

            Console.WriteLine($"Old description: {project.Description}");
            Console.Write("New description (optional): ");
            dto.Description = Console.ReadLine();
            if (dto.Description == "")
                dto.Description = project.Description;

            Console.WriteLine($"Old duration: {project.StartDate.ToShortDateString()} - {project.EndDate.ToShortDateString()}");
            Console.Write("New start date (yyyy-MM-dd): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
                dto.StartDate = startDate;
            else
                dto.StartDate = project.StartDate;

            Console.Write("New end date (yyyy-MM-dd): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                dto.EndDate = endDate;
            else
                dto.EndDate = project.EndDate;

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Available statuses: ");
            for (int i = 0; i < statuses.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {statuses[i]}");
            }
            Console.Write("Select customer: ");
            if (!int.TryParse(Console.ReadLine(), out int statusIndex) || statusIndex < 1 || statusIndex > statuses.Count)
            {
                dto.Status = project.Status.Id;
            }
            else
            {
                dto.Status = statuses[statusIndex - 1].Id;
            }

            return dto;
        }
    }
}
