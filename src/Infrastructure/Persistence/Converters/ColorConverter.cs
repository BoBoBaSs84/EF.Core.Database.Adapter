using System.Drawing;

using BB84.Extensions;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Converters;

/// <summary>
/// The <see cref="Color"/> converter.
/// </summary>
internal sealed class ColorConverter : ValueConverter<Color, string>
{
	/// <summary>
	/// Initilizes an instance of the <see cref="ColorConverter"/> class.
	/// </summary>
	public ColorConverter() : base(x => x.ToRGBHexString(), x => x.FromRGBHexString())
	{ }
}
