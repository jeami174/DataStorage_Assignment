using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation_Console_MainApplication;
using Presentation_Console_MainApplication.Interfaces;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<DataContext>(dboptions =>
            dboptions.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\DataStorage_Assignment\\Data\\Databases\\new_database.mdf;Integrated Security=True;Connect Timeout=30"));

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICustomerContactRepository, CustomerContactRepository>();
        services.AddScoped<ICustomerContactService, CustomerContactService>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();
        services.AddScoped<IStatusTypeService, StatusTypeService>();
        services.AddScoped<IUnitRepository, UnitRepository>();

        services.AddScoped<IUserInterface, ConsoleUserInterface>();
    })
    .Build();

var ui = host.Services.GetRequiredService<IUserInterface>();
await ui.ShowUIAsync();

