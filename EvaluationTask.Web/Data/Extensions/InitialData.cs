using EvaluationTask.Web.Models;
using EvaluationTask.Web.Models.Enums;

namespace EvaluationTask.Web.Data.Extensions;

internal class InitialData
{
	public static IEnumerable<CancelationRequest> CancellationRequests =>
		[
			new CancelationRequest()
			{
				RequestorName = "Winne Pooh",
				RequestDate = DateTime.Parse("11/02/2024"),
				Status = Status.Pending,
				FeeAmount = 0,
				Message = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"
			},
			new CancelationRequest()
			{
				RequestorName = "Goody Dog",
				RequestDate = DateTime.Parse("11/02/2024"),
				Status = Status.ApprovedNoFee,
				FeeAmount = 0,
				Message = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout."
			},
			new CancelationRequest()
			{
				RequestorName = "Micky Mousel",
				RequestDate = DateTime.Parse("11/02/2024"),
				Status = Status.ApprovedWithFee,
				FeeAmount = 0,
				Message = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour"
			},
			new CancelationRequest()
			{
				RequestorName = "Winne Pooh",
				RequestDate = DateTime.Parse("11/02/2024"),
				Status = Status.Declined,
				FeeAmount = 0,
				Message = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC"
			}
		];
}
