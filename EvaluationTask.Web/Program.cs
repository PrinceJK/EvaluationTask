using EvaluationTask.Web.Data;
using EvaluationTask.Web.Data.Extensions;
using EvaluationTask.Web.Filters;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.WriteTo.Console()
	.WriteTo.File(builder.Configuration.GetValue<string>("LogFilePath")!,
		rollingInterval: RollingInterval.Day,
		rollOnFileSizeLimit: true)
	.CreateLogger();

// Use Serilog as the default logging provider
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add<LoggingActionFilter>();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
else
{
	await app.InitialiseDatabaseAsync();
}
app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=CancellationRequests}/{action=Index}/{id?}");

app.Logger.LogInformation("Starting the app...New");
app.Run();