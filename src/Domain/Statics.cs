using System.Globalization;
using System.Text.RegularExpressions;

namespace Domain;

/// <summary>
/// The statics class.
/// </summary>
public static class Statics
{
	/// <summary>
	/// The <see cref="CurrentCulture"/> property.
	/// </summary>
	public static CultureInfo CurrentCulture { get; } = CultureInfo.CurrentCulture;

	/// <summary>
	/// The <see cref="InvariantCulture"/> property.
	/// </summary>
	public static CultureInfo InvariantCulture { get; } = CultureInfo.InvariantCulture;

	/// <summary>
	/// The <see cref="WhitespaceRegex"/> property.
	/// </summary>
	public static Regex WhitespaceRegex { get; } = new(@"\s+");
}
