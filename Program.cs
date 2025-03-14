using Microsoft.EntityFrameworkCore;
using MyTime.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ScoolContext>(options =>
{
    options.UseMySql("Server=localhost;User ID=Miko;Password=Alabala1234!;Database=MyTime",
        new MySqlServerVersion(new Version(8, 0, 41))); // Replace with your MySQL version
});

var app = builder.Build();


// Add a test endpoint for DB connectivity
app.MapGet("/test-db-connection", async (ScoolContext context) =>
{
    try
    {
        var canConnect = await context.Database.CanConnectAsync();
        return canConnect ? Results.Ok("Connected successfully!") : Results.Problem("Cannot connect to the database.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error: {ex.Message}");
    }
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapGet("/test-db-query", async (ScoolContext context) =>
{
    try
    {
        await context.Database.ExecuteSqlRawAsync("INSERT INTO `mytime`.`users` (`id`, `name`, `email`, `created_at`) VALUES ('3', 'Test3', 'test3@test.com', now());");
        return Results.Ok("Database query executed successfully!");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error: {ex.Message}");
    }
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
