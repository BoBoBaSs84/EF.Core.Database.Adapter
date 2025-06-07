using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BB84.Home.Presentation.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="ModelStateDictionary"/>.
/// </summary>
/// <remarks>
/// This class contains methods to simplify the retrieval and formatting of validation errors from a
/// <see cref="ModelStateDictionary"/> instance, which is commonly used in ASP.NET MVC and Web API applications.
/// </remarks>
public static class ModelStateExtensions
{
	/// <summary>
	/// Extracts error messages from the specified <see cref="ModelStateDictionary"/> and formats them as a single string.
	/// </summary>
	/// <remarks>
	/// This method is useful for summarizing validation errors in a format suitable for logging or displaying to users.
	/// </remarks>
	/// <param name="modelState">The <see cref="ModelStateDictionary"/> containing validation errors.</param>
	/// <param name="forUi">A boolean value indicating whether the errors should be formatted for user interface display.
	/// If <see langword="true"/>, only error messages are included. If <see langword="false"/>, both the field keys and their
	/// associated raw values are included.
	/// </param>
	/// <returns>
	/// A string containing the formatted error messages. If <paramref name="forUi"/> is <see langword="true"/>, the result
	/// includes only error messages. Otherwise, the result includes field keys, raw values, and error messages.
	/// </returns>
	public static string GetErrors(this ModelStateDictionary modelState, bool forUi = true)
	{
		var errorEntries = modelState
			.Where(x => x.Value != null)
			.Select(x => new { x.Key, x.Value })
			.Where(x => x.Value!.Errors.Count > 0)
			.ToList();

		List<string> result = forUi
			? [.. errorEntries.Select(y => $"{string.Join("\n", y.Value!.Errors.Select(y => y.ErrorMessage))}")]
			: [.. errorEntries.Select(y => $"[{y.Key}]=[{y.Value!.RawValue ?? "null"}]\n{string.Join("\n", y.Value.Errors.Select(y => y.ErrorMessage))}")];

		return string.Join("\n", result);
	}
}
