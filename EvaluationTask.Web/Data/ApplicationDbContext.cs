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
}
