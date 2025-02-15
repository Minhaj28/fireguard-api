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
    }
}
