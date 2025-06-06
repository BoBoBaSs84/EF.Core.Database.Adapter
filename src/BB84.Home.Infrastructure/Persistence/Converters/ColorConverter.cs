﻿using System.Drawing;

using BB84.Extensions;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BB84.Home.Infrastructure.Persistence.Converters;

/// <summary>
/// The <see cref="Color"/> converter.
/// </summary>
internal sealed class ColorConverter : ValueConverter<Color, byte[]>
{
	/// <summary>
	/// Initilizes an instance of the <see cref="ColorConverter"/> class.
	/// </summary>
	public ColorConverter() : base(x => x.ToRgbByteArray(), x => x.FromRgbByteArray())
	{ }
}
