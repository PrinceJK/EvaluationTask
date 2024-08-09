using EvaluationTask.Web.Models;
using EvaluationTask.Web.Models.Enums;

namespace EvaluationTask.Web.Data.Extensions;

internal class InitialData
{
	public static IEnumerable<CancellationRequest> CancellationRequests =>
		[
			new CancellationRequest()
			{
				RequestorName = "Winne Pooh",
				RequestDate = DateTime.Parse("11/02/2024"),
				Status = Status.Pending,
				FeeAmount = 0,
				Message = "This is a message"
			},
			new CancellationRequest()
			{
				RequestorName = "Goody Dog",
				RequestDate = DateTime.Parse("11/02/2024"),
				Status = Status.ApprovedNoFee,
				FeeAmount = 0,
				Message = "This is a message"
			},
			new CancellationRequest()
			{
				RequestorName = "Micky Mousel",
				RequestDate = DateTime.Parse("11/02/2024"),
				Status = Status.ApprovedWithFee,
				FeeAmount = 0,
				Message = "This is a message"
			},
			new CancellationRequest()
			{
				RequestorName = "Winne Pooh",
				RequestDate = DateTime.Parse("11/02/2024"),
				Status = Status.Declined,
				FeeAmount = 0,
				Message = "This is a message"
			}
		];
}
