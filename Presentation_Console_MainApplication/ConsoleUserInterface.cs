using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Interfaces;
using Presentation_Console_MainApplication.Dialogs;
using Presentation_Console_MainApplication.Interfaces;

namespace Presentation_Console_MainApplication;

public class ConsoleUserInterface : IUserInterface, IMainMenuOperations
{
    private readonly IProjectService _projectService;
    private readonly ICustomerService _customerService;
    private readonly IServiceService _serviceService;
    private readonly IUserService _userService;
    private readonly IStatusTypeService _statusTypeService;

    public ConsoleUserInterface(IProjectService projectService, ICustomerService customerService, IServiceService serviceService, IUserService userService, IStatusTypeService statusTypeService)
    {
        _projectService = projectService;
        _customerService = customerService;
        _serviceService = serviceService;
        _userService = userService;
        _statusTypeService = statusTypeService;
    }

    public async Task ShowUIAsync()
    {
        while (true)
        {
            await MainMenuDialog.ShowAsync(this);
        }
    }

    private async Task OnCreateProject(ProjectCreateDto dto)
    {
        try
        {
            bool success = await _projectService.CreateProjectAsync(dto);
            if (success)
                Console.WriteLine("Projektet skapades framgångsrikt!");
            else
                Console.WriteLine("Fel vid skapande av projekt.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid skapande av projekt: {ex.Message}");
        }
    }

    private async Task OnEditProject(int id, ProjectUpdateDto dto)
    {
        try
        {
            bool success = await _projectService.UpdateProjectAsync(id, dto);
            if (success)
                Console.WriteLine("Projektet uppdaterades framgångsrikt!");
            else
                Console.WriteLine("Fel vid uppdatering av projekt.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid uppdatering av projekt: {ex.Message}");
        }
    }

    public async Task MenuOptionCreateProjectAsync()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        var services = await _serviceService.GetAllServicesAsync();
        var users = await _userService.GetAllUsersAsync();
        var statusTypes = await _statusTypeService.GetAllStatusTypesAsync();
        await CreateProjectDialog.ShowAsync(
             customers.ToList(),
             services.ToList(),
             users.ToList(),
             statusTypes.ToList(),
             OnCreateProject);
    }


    public async Task MenuOptionEditProjectAsync()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        var statuses = await _statusTypeService.GetAllStatusTypesAsync();
        await EditProjectDialog.ShowAsync(projects.ToList(), statuses.ToList(), OnEditProject);
    }

    public async Task MenuOptionListProjectsAsync()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        await ShowProjectsDialog.ShowAsync(projects);
    }

    public async Task MenuOptionShowProjectDetailsAsync()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        await ShowProjectDialog.ShowAsync(projects.ToList());
    }

    public void MenuOptionQuit()
    {
        Console.WriteLine("Avslutar applikationen.");
        Environment.Exit(0);
    }

    public void MenuOptionInvalid()
    {
        Console.WriteLine("Ogiltigt val. Försök igen.");
        Console.WriteLine("Tryck på en tangent för att återgå till huvudmenyn...");
        Console.ReadKey();
    }
}
