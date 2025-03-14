using Microsoft.EntityFrameworkCore;
using MyTime.Model;
using static MyTime.Common.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SiteUserContext>(options =>
{
    options.UseMySql(CONNECTION_STRING, ServerVersion.AutoDetect(CONNECTION_STRING));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.MapGet("/test-db-query", async (SiteUserContext context) =>
//{
//    try
//    {
//        await context.Database.ExecuteSqlRawAsync("INSERT INTO `mytime`.`users` (`id`, `name`, `email`, `created_at`) VALUES ('4', 'Test4', 'test4@test.com', now());");
//        return Results.Ok("Database query executed successfully!");
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem($"Error: {ex.Message}");
//    }
//});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
