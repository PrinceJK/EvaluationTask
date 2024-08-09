using EvaluationTask.Web.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EvaluationTask.Web.Models;

public class CancellationRequest
{
	public int Id { get; set; }

	[Required]
	public string RequestorName { get; set; }

	[DataType(DataType.Date)]
	public DateTime RequestDate { get; set; }

	[Required]
	public Status Status { get; set; }

	[DataType(DataType.Currency)]
	public decimal? FeeAmount { get; set; }

	[Required]
    public string Message { get; set; }
}
