using Business.Dtos;
using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs;

public static class CreateNewServiceDialog
{
    public static async Task<int> GetServiceIdAsync(IServiceService serviceService, IUnitService unitService)
    {
        Console.Clear();
        Console.WriteLine("***** Create New Service *****");

        Console.Write("Service Name: ");
        string serviceName = Console.ReadLine()!.Trim();

        var units = (await unitService.GetAllUnitsAsync()).ToList();
        int unitId;
        if (units.Count == 0)
        {
            Console.WriteLine("No units available. Please add a unit first.");
            unitId = await CreateNewUnitDialog.GetUnitIdAsync(unitService);
        }
        else
        {
            Console.WriteLine("Do you want to:");
            Console.WriteLine("1. Add existing unit");
            Console.WriteLine("2. Create new unit");
            Console.Write("Enter your choice (1 or 2): ");
            var serviceChoice = Console.ReadLine();
            if (serviceChoice == "1")
            {
                unitId = SelectUnitDialog.Show(units);
            }
            else if (serviceChoice == "2")
            {
                unitId = await CreateNewUnitDialog.GetUnitIdAsync(unitService);
            }
            else
            {
                Console.WriteLine("Invalid input, defaulting to existing unit.");
                unitId = units[0].Id;
            }
        }
        
        Console.Write("Price per unit: ");
        decimal pricePerUnit = 0;
        if (!decimal.TryParse(Console.ReadLine(), out pricePerUnit))
            pricePerUnit = 1;
        
        var serviceDto = new ServiceCreateDto
        {
            ServiceName = serviceName,
            PricePerUnit = pricePerUnit,
            UnitId = unitId
        };

        bool serviceCreated = await serviceService.CreateAsync(serviceDto);
        var services = (await serviceService.GetAllServicesAsync()).ToList();
        if (serviceCreated)
        {
            var newService = services.OrderByDescending(s => s.Id).First();
            Console.WriteLine($"New service '{newService.ServiceName}' created with ID {newService.Id}.");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            return newService.Id;
        }
        else
        {
            Console.WriteLine("Failed to create new service, defaulting to existing service.");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            return services[0].Id;
        }
    }
}

