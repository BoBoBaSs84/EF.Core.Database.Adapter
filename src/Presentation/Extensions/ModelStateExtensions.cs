using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Extensions;

/// <summary>
/// Extensions methods for <see cref="ModelStateDictionary"/>
/// </summary>
public static class ModelStateExtensions
{
	public static string GetErrors(this ModelStateDictionary modelState, bool forUi = true)
	{
		var errorEntries = modelState
			.Where(x => x.Value != null)
			.Select(x => new { x.Key, x.Value })
			.Where(x => x.Value!.Errors.Count > 0)
			.ToList();

		List<string> result;

		if (forUi)
			result = errorEntries
				.Select(y =>
					$"{string.Join("\n", y.Value!.Errors.Select(y => y.ErrorMessage))}")
				.ToList();
		else
			result = errorEntries
				.Select(y =>
					$"[{y.Key}]=[{y.Value!.RawValue ?? "null"}]\n{string.Join("\n", y.Value.Errors.Select(y => y.ErrorMessage))}")
				.ToList();

		return string.Join("\n", result);
	}
}
