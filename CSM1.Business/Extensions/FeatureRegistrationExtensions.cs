using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CSM1.Business.Dtos.TopicDtos;
using CSM1.Business.Profiles;
using CSM1.Business.Repositories.Implements;
using CSM1.Business.Repositories.Interfaces;
using CSM1.Business.Services.Implements;
using CSM1.Business.Services.Interfaces;
using CSM1.Business.ExternalServices.Interfaces;
using CSM1.Business.ExternalServices.Implements;
using CSM1.Business.Dtos.AuthDtos;
using CSM1.Business.Models;
using CSM1.Core.Entities;
using CSM1.DAL.Contexts;
using Microsoft.AspNetCore.Builder;
using CSM1.Core.Entities.Static;
using System.Text;
using CSM1.Business.Exceptions.Roles;
using Microsoft.Extensions.Configuration;
using CSM1.Business.Exceptions.Common;
using CSM1.Business.Exceptions.Auth;

namespace CSM1.Business.Extensions;

public static class FeatureRegistrationExtensions
{

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITopicRepository, TopicRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();

        return services;
    }
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }

    static string ParseResultErrors(IEnumerable<IdentityError> errors)
    {
        StringBuilder sb = new StringBuilder();
        foreach (IdentityError error in errors)
        {
            sb.Append(error.Description);
            sb.Append(Environment.NewLine);
        }
        return sb.ToString().Trim();
    }
    static async Task CreateRoles(RoleManager<IdentityRole> roleManager, string[] roles)
    {
        foreach (string item in roles)
        {
            if (!await roleManager.RoleExistsAsync(item))
            {
                var result = await roleManager.CreateAsync(new IdentityRole
                {
                    Name = item
                });
                if (!result.Succeeded) throw new RoleCreateException(ParseResultErrors(result.Errors));
            }
        }
    }
    static async Task CreateUser(UserManager<AppUser> userManager, AppUserDto dto)
    {
        if (await userManager.FindByNameAsync(dto.UserName) == null) 
        {
            AppUser user = new()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                BirthDay = dto.BirthDay,
                Name = dto.Name,
                Surname = dto.Surname,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) throw new UsernameOrEmailExistException(ParseResultErrors(result.Errors));

            result = await userManager.AddToRoleAsync(user, dto.MainRole);
            if (!result.Succeeded) throw new RoleAddException(ParseResultErrors(result.Errors));
        }
    }
    public static WebApplication UseSeedDatas(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            using(var scope = context.RequestServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await CreateRoles(roleManager, Enum.GetNames<Roles.AuthRoles>());
                await CreateUser(userManager, app.Configuration.GetSection("SuperAdmin").Get<AppUserDto>());
            }

            await next();
        });

        return app;
    }


    public static IServiceCollection AddCustomIdentity(this IServiceCollection services, string connection)
    {
        services.AddDbContext<CSM1DbContext>(options =>
        {
            options.UseSqlServer(connection);
        }).AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.SignIn.RequireConfirmedEmail = true;
            opt.User.RequireUniqueEmail = true;
            opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";
            opt.Lockout.MaxFailedAccessAttempts = 5;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 4;
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<CSM1DbContext>();

        return services;
    }

    public static IServiceCollection AddJwtAuth(this IServiceCollection services, JwtTokenParameters parameters)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = ITokenService.JwtTokenValidationParametrs(parameters);
        });
        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<RegisterDtoValidator>());
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<LoginDtoValidator>());
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<TopicCreateDtoValidator>());
        services.AddAutoMapper(typeof(TopicMappingProfile).Assembly);

        return services;
    }
}
