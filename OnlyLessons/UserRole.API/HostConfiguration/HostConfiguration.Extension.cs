using Microsoft.EntityFrameworkCore;
using UserRole.Application.Services;
using UserRole.Infrostructure;
using UserRole.Persistance.DataAccess;
using UserRole.Persistance.Repositoryies.UserRepository;

namespace UserRole.API.HostConfiguration;

public static partial  class HostConfiguration
{
    private static WebApplicationBuilder AddIdentityInfrostructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthService , AuthService>();
        builder.Services.AddScoped<ITokenGeneratorService, TokenGenerateService>();
        builder.Services.AddScoped<IUserService,UserService>();
        return builder;
    }
    private static WebApplicationBuilder AddExposes(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(context=>context.LowercaseUrls=true);
        builder.Services.AddControllers();
        return builder;
    }
    private static WebApplicationBuilder AddDataAccess(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<UserRoleDbContext>((o =>
o.UseNpgsql("Host=localhost;Port=5432;Database=UserRoles;Username=postgres;Password=postgres")));
        builder.Services.AddScoped<IUserRepository,UserRepositorys>();
        return builder;
    }
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();
        builder.Services.AddEndpointsApiExplorer();
        return builder;
    }
    private static WebApplication UseExoses(this WebApplication app)
    {
        app.MapControllers();
        return app;

    }
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}
