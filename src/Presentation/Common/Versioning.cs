using Asp.Versioning;

namespace Presentation.Common;

/// <summary>
/// Static class with versioning information.
/// </summary>
public static class Versioning
{
	/// <summary>
	/// Current api veriosn as <see cref="Asp.Versioning.ApiVersion"/> object.
	/// </summary>
	public static readonly ApiVersion ApiVersion = GetCurrentApiVersion();

	/// <summary>
	/// The current version as string.
	/// </summary>
	public const string CurrentVersion = "1.0";

	private static ApiVersion GetCurrentApiVersion()
	{
		List<int> splittedVersion = CurrentVersion
			.Split('.')
			.Where(x => int.TryParse(x, out _))
			.Select(int.Parse)
			.ToList();

		return new ApiVersion(splittedVersion[0], splittedVersion[1]);
	}
}
