using System.Text.RegularExpressions;

namespace EvaluationTask.Web.Extensions;

public static class EnumExtensions
{
	public static string GetDisplayName(this Enum enumValue)
	{
		return Regex.Replace(enumValue.ToString(), "([a-z])([A-Z])", "$1 $2");
	}
}
