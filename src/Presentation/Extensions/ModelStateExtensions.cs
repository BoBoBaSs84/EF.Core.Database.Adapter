using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Extensions;

/// <summary>
/// Extensions methods for <see cref="ModelStateDictionary"/>
/// </summary>
public static class ModelStateExtensions
{
	/// <summary>
	/// Should retrieve the model state errors.
	/// </summary>
	/// <param name="modelState"></param>
	/// <param name="forUi"></param>
	/// <returns>The erros as concated string.</returns>
	public static string GetErrors(this ModelStateDictionary modelState, bool forUi = true)
	{
		var errorEntries = modelState
			.Where(x => x.Value != null)
			.Select(x => new { x.Key, x.Value })
			.Where(x => x.Value!.Errors.Count > 0)
			.ToList();

		List<string> result = forUi
			? errorEntries
				.Select(y => $"{string.Join("\n", y.Value!.Errors.Select(y => y.ErrorMessage))}")
				.ToList()
			: errorEntries
				.Select(y => $"[{y.Key}]=[{y.Value!.RawValue ?? "null"}]\n{string.Join("\n", y.Value.Errors.Select(y => y.ErrorMessage))}")
				.ToList();

		return string.Join("\n", result);
	}
}
