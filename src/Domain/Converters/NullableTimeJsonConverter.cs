using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Converters;

/// <summary>
/// The <see cref="TimeSpan"/> json converter class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="JsonConverter{T}"/> class.
/// This is for the nullable <see cref="TimeSpan"/> type.
/// </remarks>
public sealed class NullableTimeJsonConverter : JsonConverter<TimeSpan?>
{
	/// <inheritdoc/>
	public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		string? value = reader.GetString();
		return string.IsNullOrWhiteSpace(value) ? null : TimeSpan.Parse(value, CultureInfo.CurrentCulture);
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
	{
		if (value is null)
			return;

		writer.WriteStringValue(value.Value.ToString("c"));
	}
}
