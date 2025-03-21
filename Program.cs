using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyTime.MailModel;
using MyTime.Model;
using static MyTime.Common.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SiteUserContext>(options =>
{
    options.UseMySql(CONNECTION_STRING, ServerVersion.AutoDetect(CONNECTION_STRING));
});

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(30);
    options.ExcludedHosts.Add("time4my.life");
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

//builder.Services.AddHttpsRedirection(options =>
//{
//    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
//    options.HttpsPort = 5004;
//});


builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();

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

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("An error occurred.");
    });
});

app.UseResponseCompression();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages();//  .WithStaticAssets();

app.Run();