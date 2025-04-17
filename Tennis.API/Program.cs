using Wolverine;
using Tennis.EventStore;
using Tennis.Dominio;
using Wolverine.Marten;
using Tennis.API;

var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();
// Add services to the container.
var martenConnectionString = builder.Configuration.GetConnectionString("TenisEventStore") ??
                             throw new ArgumentNullException(
                                 "La cadena de conexi�n 'TenisEventStore' no est� configurada.");


builder.Services
    .AddHealthChecks()
    .AddNpgSql(martenConnectionString);

builder.UseWolverine(options =>
{
    options.Discovery.IncludeAssembly(typeof(IEventStore).Assembly);
    options.Services.AddMartenConfiguration(builder.Configuration).IntegrateWithWolverine();
    options.Policies.AutoApplyTransactions();
    options.Durability.Mode = DurabilityMode.MediatorOnly;
}
);

builder.Services.AddMartenEventStore();
builder.Services.AddScoped<ICommandRouter, WolverineCommandRouter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (isDevelopment)
{
    app.MapOpenApi();
}
app.UseHealthChecks("/health");

app.UseHttpsRedirection();


app.Run();

