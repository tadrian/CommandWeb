using CommandWeb;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using CommandWeb.Data;


var builder = WebApplication.CreateBuilder(args);

// Restrict Kestrel to localhost only
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000); // Only binds to localhost
});


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


// Register command execution service
builder.Services.AddSingleton<CommandWeb.Services.ICommandExecutionService, CommandWeb.Services.CommandExecutionService>();

// Register custom command CRUD service
builder.Services.AddSingleton<CommandWeb.Services.ICustomCommandService, CommandWeb.Services.CustomCommandService>();

// Register Radzen services for dialogs, notifications, tooltips, and context menus
builder.Services.AddScoped<Radzen.DialogService>();
builder.Services.AddScoped<Radzen.NotificationService>();
builder.Services.AddScoped<Radzen.TooltipService>();
builder.Services.AddScoped<Radzen.ContextMenuService>();



// Ensure static web assets are enabled (fixes missing _content assets in some .NET 10/preview scenarios)
builder.WebHost.UseStaticWebAssets();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}



app.UseStaticFiles();


app.UseRouting();

// Add antiforgery middleware as required by .NET 10 for endpoints with antiforgery metadata
app.UseAntiforgery();



// Map SignalR CommandHub
app.MapHub<CommandWeb.Hubs.CommandHub>("/commandHub");


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();
