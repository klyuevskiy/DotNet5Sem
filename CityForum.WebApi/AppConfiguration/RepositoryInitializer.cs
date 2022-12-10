using System;
using CityForum.Entities;
using CityForum.Entities.Models;
using CityForum.Repository;
using CityForum.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CityForum.WebApi.AppConfiguration;

public static class RepositoryInitializer
{
    public const string MASTER_ADMIN_LOGIN = "master";
    public const string MASTER_ADMIN_PASSWORD = "master";

    private static async Task CreateGlobalAdmin(IAuthService authService)
    {
        await authService.RegisterUser(new Services.Models.RegisterUserModel()
        {
            Login = MASTER_ADMIN_LOGIN,
            Password = MASTER_ADMIN_PASSWORD,
        });
    }

    public static async Task InitializeRepository(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var usersRepository = (IRepository<User>)scope.ServiceProvider.GetRequiredService(typeof(IRepository<User>));
            var user = usersRepository.GetAll().Where(x => x.Login == MASTER_ADMIN_LOGIN).FirstOrDefault();
            if (user == null)
            {
                var authService = (IAuthService)scope.ServiceProvider.GetRequiredService(typeof(IAuthService));
                await CreateGlobalAdmin(authService);
            }
        }
    }
}