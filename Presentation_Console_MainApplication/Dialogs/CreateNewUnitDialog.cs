using Business.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs;

public static class CreateNewUnitDialog
{
    public static async Task<int> GetUnitIdAsync(IUnitService unitService)
    {
        Console.Clear();
        Console.WriteLine("***** Create New Unit *****");

        Console.Write("Unit Name: ");
        string name = Console.ReadLine()!.Trim();

        bool created = await unitService.CreateUnitAsync(name);
        var units = (await unitService.GetAllUnitsAsync()).ToList();
        if (created)
        {
            var newUnit = units.OrderByDescending(s => s.Id).First();
            Console.WriteLine($"New service '{newUnit.UnitName}' created with ID {newUnit.Id}.");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            return newUnit.Id;
        }
        else
        {
            Console.WriteLine("Failed to create new service, defaulting to existing service.");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            return units[0].Id;
        }
    }
}