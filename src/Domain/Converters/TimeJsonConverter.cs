using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Converters;

/// <summary>
/// The <see cref="TimeSpan"/> json converter class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="JsonConverter{T}"/> class.
/// This is for the <see cref="TimeSpan"/> type.
/// </remarks>
public sealed class TimeJsonConverter : JsonConverter<TimeSpan>
{
	/// <inheritdoc/>
	public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		string? value = reader.GetString();
		return string.IsNullOrWhiteSpace(value) ? new() : TimeSpan.Parse(value, CultureInfo.CurrentCulture);
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
		=> writer.WriteStringValue(value.ToString("hh:mm:ss"));
}
