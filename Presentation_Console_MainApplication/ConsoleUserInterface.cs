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
    private readonly IUserRoleService _userRoleService;
    private readonly ICustomerContactService _customerContactService;
    private readonly IUnitService _unitService;

    public ConsoleUserInterface(
        IProjectService projectService, ICustomerService customerService, IServiceService serviceService, 
        IUserService userService, IStatusTypeService statusTypeService, IUserRoleService userRoleService,
        ICustomerContactService customerContactService, IUnitService unitService)
    {
        _projectService = projectService;
        _customerService = customerService;
        _serviceService = serviceService;
        _userService = userService;
        _statusTypeService = statusTypeService;
        _userRoleService = userRoleService;
        _customerContactService = customerContactService;
        _unitService = unitService;
    }

    public async Task ShowUIAsync()
    {
        _statusTypeService.EnsureDefaultStatusTypes();

        while (true)
        {
            await MainMenuDialog.ShowAsync(this);
        }
    }

    public async Task MenuOptionCreateProjectAsync()
    {
        await CreateNewProjectDialog.CreateProjectAsync(_projectService, _customerService, _serviceService, _userService, _customerContactService, _userRoleService, _unitService, _statusTypeService);
    }


    public async Task MenuOptionEditProjectAsync()
    {
        await EditProjectDialog.EditProjectAsync(_projectService, _customerService, _customerContactService, _serviceService, _userService, _userRoleService, _statusTypeService);
    }

    public async Task MenuOptionListProjectsAsync()
    {
        var projects = await _projectService.GetAllProjectsWithDetailsAsync();
        ShowProjectsDialog.Show(projects);
    }

    public async Task MenuOptionShowProjectDetailsAsync()
    {
        await ShowProjectDialog.ShowAsync(_projectService);
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

    public async Task MenuOptionDeleteProjectAsync()
    {
        await DeleteProjectDialog.ShowAsync(_projectService);
    }
}
