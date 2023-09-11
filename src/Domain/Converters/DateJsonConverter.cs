using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Converters;

/// <summary>
/// The date json converter class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="JsonConverter{T}"/> class.
/// This is for the <see cref="DateTime"/> type.
/// </remarks>
public sealed class DateJsonConverter : JsonConverter<DateTime>
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
