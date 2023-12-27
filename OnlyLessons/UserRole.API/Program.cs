

using Microsoft.EntityFrameworkCore;
using UserRole.API.HostConfiguration;
using UserRole.Persistance.DataAccess;

var builder = WebApplication.CreateBuilder(args);

await builder.ConfigureAsync();

var app = builder.Build();

await app.ConfigureAsync();

app.Run();
