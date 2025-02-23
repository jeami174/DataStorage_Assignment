using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs;

public static class SelectStatusTypeDialog
{
    public static int Show(IList<StatusTypeModel> statusTypes)
    {
        Console.Clear();
        Console.WriteLine("Select project status:");
        for (int i = 0; i < statusTypes.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {statusTypes[i].StatusTypeName}");
        }
        Console.Write("Select status (enter number): ");
        if (!int.TryParse(Console.ReadLine(), out int index) ||
            index < 1 || index > statusTypes.Count)
        {
            Console.WriteLine("Invalid input, using status #1");
            return statusTypes[0].Id;
        }
        else
        {
            return statusTypes[index - 1].Id;
        }
    }
}
