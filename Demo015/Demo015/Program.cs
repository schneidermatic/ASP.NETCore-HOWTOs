using Demo015;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Custom Services which are needed in the HomeController.cs
builder.Services.AddHostedService<LongRunningService>();
builder.Services.AddSingleton<BackgroundWorkerQueue>();

// Custom Services that is needed for logging in Program.cs
ILogger logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

logger.LogInformation($"I'm still running at {DateTime.UtcNow.TimeOfDay}");

app.Run();
