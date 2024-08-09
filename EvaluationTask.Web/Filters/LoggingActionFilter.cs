using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace EvaluationTask.Web.Filters;

public class LoggingActionFilter(ILogger<LoggingActionFilter> _logger) : IActionFilter
{
	private Stopwatch _timer;
	public void OnActionExecuted(ActionExecutedContext context)
	{
		_timer.Stop();
		var timeTaken = _timer.Elapsed;

		if (timeTaken.Seconds > 3)
		{
			_logger.LogWarning("[PERFORMANCE] The action {ActionName} took {TimeTaken} seconds.",
				context.ActionDescriptor.DisplayName, timeTaken.Seconds);
		}

		_logger.LogInformation("[END] Executed action {ActionName}", context.ActionDescriptor.DisplayName);
	}

	public void OnActionExecuting(ActionExecutingContext context)
	{
		_logger.LogInformation("[START] Executing action {ActionName} with parameters {Parameters}",
			context.ActionDescriptor.DisplayName, context.ActionArguments);

		_timer = Stopwatch.StartNew();
	}
}
