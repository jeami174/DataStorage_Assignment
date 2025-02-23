using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs;

internal class SelectUnitDialog
{
    public static int Show(IList<UnitModel> units)
    {
        if (units.Count == 0)
        {
            Console.WriteLine("No units available.");
            return -1;
        }

        Console.WriteLine("Available units:");
        for (int i = 0; i < units.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {units[i].UnitName}");
        }
        Console.Write("Select unit (enter number): ");
        if (!int.TryParse(Console.ReadLine(), out int index) ||
            index < 1 || index > units.Count)
        {
            Console.WriteLine("Invalid input, using unit #1");
            return units[0].Id;
        }
        else
        {
            return units[index - 1].Id;
        }
    }
}
