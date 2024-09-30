using System.Text.Json;
using System.Text.Json.Serialization;

using BB84.Extensions;

namespace Application.Converters;

/// <summary>
/// The <see cref="byte"/> array json converter class.
/// </summary>
internal sealed class ByteArrayJsonConverter : JsonConverter<byte[]>
{
	/// <inheritdoc/>
	public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		string? value = reader.GetString();
		return string.IsNullOrWhiteSpace(value) ? [] : value.FromBase64();
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
		=> writer.WriteStringValue(value.ToBase64());
}

/// <summary>
/// The nullable <see cref="byte"/> array json converter class.
/// </summary>
internal sealed class NullableByteArrayJsonConverter : JsonConverter<byte[]?>
{
	/// <inheritdoc/>
	public override byte[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		string? value = reader.GetString();
		return string.IsNullOrWhiteSpace(value) ? null : value.FromBase64();
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, byte[]? value, JsonSerializerOptions options)
	{
		if (value is not null)
			writer.WriteStringValue(value.ToBase64());
		else
			writer.WriteNullValue();
	}
}