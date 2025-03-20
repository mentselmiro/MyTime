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

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseHsts();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

app.UseResponseCompression();