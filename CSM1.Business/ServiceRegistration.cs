﻿using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using CSM1.Business.Dtos.TopicDtos;
using CSM1.Business.Profiles;
using CSM1.Business.Repositories.Implements;
using CSM1.Business.Repositories.Interfaces;
using CSM1.Business.Services.Implements;
using CSM1.Business.Services.Interfaces;
using CSM1.Business.ExternalServices.Interfaces;
using CSM1.Business.ExternalServices.Implements;

namespace CSM1.Business;

public static class ServiceRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITopicRepository, TopicRepository>();
        return services;
    }
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<TopicCreateDtoValidator>());
        services.AddAutoMapper(typeof(TopicMappingProfile).Assembly);
        return services;
    }
}
