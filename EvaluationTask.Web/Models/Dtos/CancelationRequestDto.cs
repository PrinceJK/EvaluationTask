using EvaluationTask.Web.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EvaluationTask.Web.Models.Dtos;

public class CancelationRequestDto
{
    public int Id { get; set; }
    public string RequestorName { get; set; }
    [DataType(DataType.Date)]
    public DateTime RequestDate { get; set; }
    public Status Status { get; set; }
    [DataType(DataType.Currency)]
    public decimal FeeAmount { get; set; }
    public string Message { get; set; }
}
