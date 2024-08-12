using EvaluationTask.Web.Models.Enums;

namespace EvaluationTask.Web.Models.Dtos;

public class CancellationRequestDto
{
    public string RequestorName { get; set; }
    public DateTime RequestDate { get; set; }
    public Status Status { get; set; }
    public decimal FeeAmount { get; set; }
    public string Message { get; set; }
}
