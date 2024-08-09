using Microsoft.EntityFrameworkCore;

namespace EvaluationTask.Web.Data.Extensions;

public static class DatabaseExtensions
{
	public static async Task InitialiseDatabaseAsync(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		context.Database.MigrateAsync().GetAwaiter().GetResult();

		await SeedAsync(context);
	}

	private static async Task SeedAsync(ApplicationDbContext context)
	{
		await CancellationRequest(context);
	}

	private static async Task CancellationRequest(ApplicationDbContext context)
	{
		if (!await context.CancellationRequests.AnyAsync())
		{
			await context.CancellationRequests.AddRangeAsync(InitialData.CancellationRequests);
			await context.SaveChangesAsync();
		}
	}
}