namespace UserRole.API.HostConfiguration;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder>ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddIdentityInfrostructure()
            .AddDataAccess()
            .AddDevTools()
            
            .AddExposes();
        return new( builder);
    }
    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UseDevTools()
            .UseExoses();
        return new( app);
    }
}
