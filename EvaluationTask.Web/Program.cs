using EvaluationTask.Web.Filters;
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
builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add<LoggingActionFilter>();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Logger.LogInformation("Starting the app...New");
app.Run();