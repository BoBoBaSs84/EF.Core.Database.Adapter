using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Converters.JsonConverters;

/// <summary>
/// The time span json converter class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="JsonConverter{T}"/> class.
/// </remarks>
public sealed class TimeSpanJsonConverter : JsonConverter<TimeSpan?>
{
	/// <inheritdoc/>
	public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		string? value = reader.GetString();
		return string.IsNullOrWhiteSpace(value) ? null : TimeSpan.Parse(value, Domain.Statics.CurrentCulture);
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
	{
		if (value is null)
			return;

		writer.WriteStringValue(value.ToString());
	}
}
