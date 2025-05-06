using Microsoft.EntityFrameworkCore;
using MyTime.MailModel;
using MyTime.Model;
using static MyTime.Common.Constants;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
var mailPassword = configuration["MailSettings:Password"];

// Add services to the container.
// Registers Razor Pages as a service to enable Razor Pages functionality.
builder.Services.AddRazorPages();

// Configures the database context to use MySQL with Entity Framework Core.
// The connection string and server version are automatically detected.
builder.Services.AddDbContext<SiteUserContext>(options =>
{
    options.UseMySql(
        connectionString, 
        ServerVersion.AutoDetect(connectionString)
    );
});

// Configures HTTP Strict Transport Security (HSTS) to enforce HTTPS connections.
builder.Services.AddHsts(options =>
{
    options.Preload = true; // Indicates that the domain should be preloaded in browsers' HSTS lists.
    options.IncludeSubDomains = true; // Applies HSTS to all subdomains.
    options.MaxAge = TimeSpan.FromDays(30); // Specifies the duration for which HSTS is enforced.
    options.ExcludedHosts.Add("time4my.life"); // Excludes specific hosts from HSTS enforcement.
});

// Adds response compression to reduce the size of HTTP responses and improve performance.
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // Enables compression only for HTTPS requests.
});

// Configures the MailSettings section from the configuration file (e.g., appsettings.json).
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

// Registers the MailService as a transient service for dependency injection.
builder.Services.AddTransient<IMailService, MailService>();

// Builds the application with the configured services and middleware.
var app = builder.Build();

// Enables serving static files from the wwwroot folder.
app.UseStaticFiles();

// Redirects all HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
// In non-development environments, use a custom error page for exception handling.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Redirects to the /Error page for unhandled exceptions.
}

// Enforces HSTS to ensure secure HTTPS connections.
app.UseHsts();

// Adds a custom exception handler to return a generic error message for unhandled exceptions.
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError; // Sets the response status code to 500.
        await context.Response.WriteAsync("An error occurred."); // Writes a generic error message to the response.
    });
});

// Enables response compression middleware to optimize performance.
app.UseResponseCompression();

// Enables routing middleware to map incoming requests to endpoints.
app.UseRouting();

// Adds authorization middleware to enforce access control.
app.UseAuthorization();

// Maps static assets to endpoints (likely a custom method defined elsewhere).
app.MapStaticAssets();

// Maps Razor Pages to endpoints, enabling them to handle requests.
app.MapRazorPages();

// Starts the application and begins listening for incoming HTTP requests.
app.Run();