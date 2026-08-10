var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var startTime = DateTime.UtcNow;

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/hello", () =>
{
    var version = System.Reflection.Assembly.GetExecutingAssembly()
        .GetName().Version?.ToString() ?? "1.0.0";
    var uptime = DateTime.UtcNow - startTime;

    return Results.Json(new
    {
        message = "Hello from .NET running on your self-hosted runner!",
        serverTime = DateTime.Now.ToString("dddd, dd MMMM yyyy · HH:mm:ss"),
        framework = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription,
        environment = app.Environment.EnvironmentName,
        version = version,
        commit = Environment.GetEnvironmentVariable("GIT_COMMIT") ?? "local",
        uptime = $"{(int)uptime.TotalHours}h {uptime.Minutes}m {uptime.Seconds}s"
    });
});

app.Run();

// lets the test project reference the app for endpoint tests
public partial class Program { }