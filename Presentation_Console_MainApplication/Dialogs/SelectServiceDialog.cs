using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs;

internal class SelectServiceDialog
{
    public static int Show(IList<ServiceModel> services)
    {
        if (services.Count == 0)
        {
            Console.WriteLine("No services available.");
            return -1;
        }

        Console.WriteLine("Available services:");
        for (int i = 0; i < services.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {services[i].ServiceName}");
        }
        Console.Write("Select service (enter number): ");
        if (!int.TryParse(Console.ReadLine(), out int serviceIndex) ||
            serviceIndex < 1 || serviceIndex > services.Count)
        {
            Console.WriteLine("Invalid input, using service #1");
            return services[0].Id;
        }
        else
        {
            return services[serviceIndex - 1].Id;
        }
    }
}
