using Microsoft.EntityFrameworkCore;
using Sms.Infrastructure.Persistanse.DataContext;
using Sms.Infrostucture.Infrastructure.Common.Notifications.Broker;
using Sms.Infrostucture.Infrastructure.Common.Service;
using Sms.Infrustructure.Application.Common.Notification.Broker;
using Sms.Infrustructure.Application.Common.Notification.Service;
using System.Reflection;

namespace Sms.Infrastructure.Api.Configuration
{
    public static partial class HostConfiguration
    {
        private static WebApplicationBuilder AddNotificationInfrosturcture(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<NotificationDbContext>(option=>option.UseNpgsql(builder.Configuration.GetConnectionString("DefaoultConnection")));
            builder.Services.
                AddScoped<ISmsSenderBroker, TwilioSmsSenderBroker>();

            builder.Services
                .AddScoped<ISmsSenderBroker, SmsSenderService>();
            builder.Services
                .AddScoped<ISmsOrchestrationService, SmsOrchestrationService>()
                .AddScoped<INotificationAggregatorService, NotificationAggregatorService>();
            return builder;
        }
        private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
        {
            var assambles = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
            assambles.Add(Assembly.GetExecutingAssembly());
            builder.Services.AddAutoMapper(assambles);
            return builder;
        }
        private static WebApplicationBuilder AddExposes(this WebApplicationBuilder builder)
        {
            builder.Services.AddRouting(option => option.LowercaseUrls = true);
            builder.Services.AddControllers();
            return builder;
        }
        private static WebApplication UseExposes(this WebApplication app)
        {
            app.MapControllers();
            return app;
        }
    }
}