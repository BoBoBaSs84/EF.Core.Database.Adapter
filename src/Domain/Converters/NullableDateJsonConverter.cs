using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Converters;

/// <summary>
/// The date json converter class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="JsonConverter{T}"/> class.
/// This is for the nullable <see cref="DateTime"/> type.
/// </remarks>
public sealed class NullableDateJsonConverter : JsonConverter<DateTime?>
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
		if (value is null)
			return;

		writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd"));
	}
}
