using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.ORM;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Register services with the dependency injection container
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IBuildingRepository, BuildingRepository>();
        services.AddScoped<IBuildingService, BuildingService>();

        services.AddScoped<IFloorRepository, FloorRepository>();
        services.AddScoped<IFloorService, FloorService>();

        services.AddScoped<ICameraRepository, CameraRepository>();
        services.AddScoped<ICameraService, CameraService>();

        services.AddScoped<ISensorRepository, SensorRepository>();
        services.AddScoped<ISensorService, SensorService>();

        services.AddScoped<IIncidentRepository, IncidentRepository>();
        services.AddScoped<IIncidentService, IncidentService>();

        services.AddScoped<IEmergencyActionRepository, EmergencyActionRepository>();
        services.AddScoped<IEmergencyActionService, EmergencyActionService>();

    }
}
