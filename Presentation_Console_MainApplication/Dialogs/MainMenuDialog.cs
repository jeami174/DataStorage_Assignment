using System;
using System.Threading.Tasks;
using Presentation_Console_MainApplication.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs
{
    public static class MainMenuDialog
    {
        public static async Task ShowAsync(IMainMenuOperations handler)
        {
            Console.Clear();
            Console.WriteLine("---------- MAIN MENU ----------");
            Console.WriteLine($"{"1.",-4} Create a Project");
            Console.WriteLine($"{"2.",-4} Edit a Project");
            Console.WriteLine($"{"3.",-4} List Projects");
            Console.WriteLine($"{"4.",-4} Show Project Details");
            Console.WriteLine($"{"Q.",-4} Quit Application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Choose your menu option: ");

            var option = Console.ReadLine();

            switch (option?.ToLower())
            {
                case "q":
                    handler.MenuOptionQuit();
                    break;
                case "1":
                    await handler.MenuOptionCreateProjectAsync();
                    break;
                case "2":
                    await handler.MenuOptionEditProjectAsync();
                    break;
                case "3":
                    await handler.MenuOptionListProjectsAsync();
                    break;
                case "4":
                    await handler.MenuOptionShowProjectDetailsAsync();
                    break;
                default:
                    handler.MenuOptionInvalid();
                    break;
            }
        }
    }
}

