using EvaluationTask.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaluationTask.Web.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<CancelationRequest> CancellationRequests { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CancelationRequest>(entity =>
		{
			entity.HasKey(c => c.Id);
			entity.Property(entity => entity.Id).ValueGeneratedOnAdd();
		});
		base.OnModelCreating(modelBuilder);
	}
}
