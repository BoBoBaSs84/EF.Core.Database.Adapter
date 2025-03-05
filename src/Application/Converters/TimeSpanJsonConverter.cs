using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BB84.Home.Application.Converters;

/// <summary>
/// The <see cref="TimeSpan"/> json converter class.
/// </summary>
internal sealed class TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
	/// <inheritdoc/>
	public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		string? value = reader.GetString();
		return string.IsNullOrWhiteSpace(value) ? TimeSpan.MinValue : TimeSpan.Parse(value, CultureInfo.InvariantCulture);
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
		=> writer.WriteStringValue(value.ToString("c"));
}

/// <summary>
/// The nullable <see cref="TimeSpan"/> json converter class.
/// </summary>
internal sealed class NullableTimeSpanJsonConverter : JsonConverter<TimeSpan?>
{
	/// <inheritdoc/>
	public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		string? value = reader.GetString();
		return string.IsNullOrWhiteSpace(value) ? null : TimeSpan.Parse(value, CultureInfo.InvariantCulture);
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
	{
		if (value is not null)
			writer.WriteStringValue(value.Value.ToString("c"));
		else
			writer.WriteNullValue();
	}
}