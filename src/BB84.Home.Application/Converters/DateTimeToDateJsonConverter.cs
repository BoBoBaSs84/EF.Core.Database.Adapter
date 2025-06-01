using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BB84.Home.Application.Converters;

/// <summary>
/// The <see cref="DateTime"/> json converter class.
/// </summary>
public sealed class DateTimeJsonConverter : JsonConverter<DateTime>
{
	/// <inheritdoc/>
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		string? value = reader.GetString();
		return string.IsNullOrWhiteSpace(value) ? DateTime.MinValue : DateTime.Parse(value, CultureInfo.InvariantCulture);
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
		=> writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
}

/// <summary>
/// The nullable <see cref="DateTime"/> json converter class.
/// </summary>
public sealed class NullableDateTimeJsonConverter : JsonConverter<DateTime?>
{
	/// <inheritdoc/>
	public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		string? value = reader.GetString();
		return string.IsNullOrWhiteSpace(value) ? null : DateTime.Parse(value, CultureInfo.InvariantCulture);
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
	{
		if (value is not null)
			writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd"));
		else
			writer.WriteNullValue();
	}
}