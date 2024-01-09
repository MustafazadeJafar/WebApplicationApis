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

namespace CSM1.Business;

public static class ServiceRegistration
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
