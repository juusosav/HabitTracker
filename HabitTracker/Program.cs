using Blazored.LocalStorage;
using HabitTracker.Components;
using HabitTracker.Components.Data;
using HabitTracker.Components.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var projectRoot = builder.Environment.ContentRootPath;

var dbPath = Path.Combine(projectRoot, "Habit.db");

builder.Services.AddDbContext<HabitContext>(options =>
    options.UseSqlite($"Data Source={dbPath}")
);

builder.Services.AddScoped<HabitService>();

builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

//This creates the database
CreateDbIfNotExists(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();


//A method that creates the database using the initializer class
static void CreateDbIfNotExists(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HabitContext>();
    DbInitializer.Initialize(context);
}
