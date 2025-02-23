using System.Threading.Tasks;
using Presentation_Console_MainApplication.Dialogs;

namespace Presentation_Console_MainApplication.Interfaces
{
    /// <summary>
    /// Interface för de operationer som kan utföras från huvudmenyn i applikationen.
    /// Alla operationer som involverar databasanrop eller andra asynkrona processer är asynkrona.
    /// </summary>
    public interface IMainMenuOperations
    {
        /// <summary>
        /// Skapar ett nytt projekt.
        /// </summary>
        Task MenuOptionCreateProjectAsync();

        /// <summary>
        /// Redigerar ett befintligt projekt.
        /// </summary>
        Task MenuOptionEditProjectAsync();

        /// <summary>
        /// Visar en lista med alla projekt (översikt).
        /// </summary>
        Task MenuOptionListProjectsAsync();

        /// <summary>
        /// Visar detaljer för ett enskilt projekt.
        /// </summary>
        Task MenuOptionShowProjectDetailsAsync();

        /// <summary>
        /// Tar bort ett befintligt projekt.
        /// </summary>
        Task MenuOptionDeleteProjectAsync();

        /// <summary>
        /// Avslutar applikationen.
        /// </summary>
        void MenuOptionQuit();

        /// <summary>
        /// Hanterar ogiltiga menyval.
        /// </summary>
        void MenuOptionInvalid();

 

    }
}
