﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Kesh.Api.Data;
using Kesh.Application.Common.Identity.Service;
using Kesh.Application.Common.Notification.Service;
using Kesh.Application.Common.Verification.Service;
using Kesh.Application.RequestContexts.Broker;
using Kesh.Domain.Broker;
using Kesh.Infrostructure.Common.Identity.Service;
using Kesh.Infrostructure.Common.Notification;
using Kesh.Infrostructure.Common.RequestContexts.Broker;
using Kesh.Infrostructure.Common.Setting;
using Kesh.Infrostructure.Common.Verification.Service;
using Kesh.Persistance.DataCantext;
using Kesh.Persistance.Interceptor;
using Kesh.Persistance.Repositories;
using Kesh.Persistance.Repositories.Interfase;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Kesh.Api.HostConfiguration;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        // register configurations 
        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));

        // register fluent validation
        builder.Services.AddValidatorsFromAssemblies(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        // register automapper
        builder.Services.AddAutoMapper(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddRequestContextTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IRequestUserContextProvider, RequestUserContextProvider>()
            .AddScoped<IRequestContextProvider, RequestContextProvider>();

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<UpdateAuditableInterceptor>().AddScoped<UpdateSoftDeletedInterceptor>();

        return builder;
    }

    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        // register configurations 
        builder.Services.Configure<SmtpEmailSenderSettings>(builder.Configuration.GetSection(nameof(SmtpEmailSenderSettings)));

        // register other higher services
        builder.Services.AddScoped<IEmailOrchestrationService, EmailOrchestrationService>();

        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        // register configurations
        builder.Services.Configure<PasswordValidationSettings>(builder.Configuration.GetSection(nameof(PasswordValidationSettings)));

        // register db contexts
        builder.Services.AddDbContext<IdentityDbContext>(
            (provider, options) =>
            {
                options.UseNpgsql (builder.Configuration.GetConnectionString("DefaultConnection"));

                var serviceScope = provider.CreateScope().ServiceProvider;
                options.AddInterceptors(serviceScope.GetRequiredService<UpdateAuditableInterceptor>());
                options.AddInterceptors(serviceScope.GetRequiredService<UpdateSoftDeletedInterceptor>());
            }
        );

        // register repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>().AddScoped<IRoleRepository, RoleRepository>();

        // register helper foundation services
        builder.Services.AddTransient<IPasswordHasherService, PasswordHasherService>()
            .AddTransient<IPasswordGeneratorService, PasswordGeneratorService>();

        // register foundation data access services
        builder.Services.AddScoped<IUserService, UserService>().AddScoped<IRoleService, RoleService>();

        // register other higher services
        builder.Services.AddScoped<IAccountAggregatorService, AccountAggregatorService>()
            .AddScoped<IAuthAggregationService, AuthAggregationService>();

        return builder;
    }

    private static WebApplicationBuilder AddVerificationInfrastructure(this WebApplicationBuilder builder)
    {
        // register configurations
        builder.Services.Configure<VerificationSettings>(builder.Configuration.GetSection(nameof(VerificationSettings)));

        // register repositories
        builder.Services.AddScoped<IUserInfoVerificationCodeRepository, UserInfoVerificationCodeRepository>();

        // register foundation data access services
        builder.Services.AddScoped<IUserInfoVerificationCodeService, UserInfoVerificationCodeService>();
        builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
        builder.Services.AddScoped<IAccessTokenRepository, AccessTokenRepository>();
        builder.Services.AddScoped<IAccessTokenGeneratorService, AccessTokenGeneratorService>();

        // register other higher services
        builder.Services.AddScoped<IVerificationProcessingService, VerificationProcessingService>();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();

        return builder;
    }

    private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();
        await serviceScope.ServiceProvider.InitializeSeedAsync();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}
