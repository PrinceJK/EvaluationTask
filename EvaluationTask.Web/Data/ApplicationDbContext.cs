using EvaluationTask.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaluationTask.Web.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<CancellationRequest> CancellationRequests { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CancellationRequest>(entity =>
		{
			entity.HasKey(c => c.Id);
			entity.Property(entity => entity.Id).ValueGeneratedOnAdd();
		});
		base.OnModelCreating(modelBuilder);
	}
}
