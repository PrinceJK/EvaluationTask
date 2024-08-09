using System.ComponentModel.DataAnnotations;

namespace EvaluationTask.Web.Models;

public class CancellationRequest
{
	public int Id { get; set; }

	[Required]
	[StringLength(100)]
	public string RequestorName { get; set; }

	[DataType(DataType.Date)]
	public DateTime RequestDate { get; set; }

	[StringLength(100)]
	public string Status { get; set; }

	public decimal? FeeAmount { get; set; }
}
