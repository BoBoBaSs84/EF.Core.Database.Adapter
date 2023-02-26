using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Extensions;

/// <summary>
/// The JSON extension class.
/// </summary>
public static class JsonExtension
{
	/// <summary>
	/// The standard JSON serializer options.
	/// </summary>
	public static JsonSerializerOptions SerializerOptions => new()
	{
		WriteIndented = true,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		PropertyNameCaseInsensitive = true,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase
	};

	/// <summary>
	/// Converts an object to its serialized JSON format.
	/// </summary>
	/// <typeparam name="T">The type of object we are operating on.</typeparam>
	/// <param name="value">The object we are operating on.</param>
	/// <param name="options">The json serializer options to use.</param>
	/// <returns>The JSON string representation of the object <typeparamref name="T"/>.</returns>
	public static string ToJsonString<T>(this T value, JsonSerializerOptions? options = null)
		=> JsonSerializer.Serialize(value, typeof(T), options);

	/// <summary>
	/// Creates an object instance from the specified JSON string.
	/// </summary>
	/// <typeparam name="T">The Type of the object we are operating on.</typeparam>
	/// <param name="value">The object we are operating on</param>
	/// <param name="jsonString">The JSON string to deserialize from.</param>
	/// <param name="options">The json serializer options to use.</param>
	/// <returns>An object instance.</returns>
	public static T FromJsonString<T>(this T value, string jsonString, JsonSerializerOptions? options = null)
		=> JsonSerializer.Deserialize<T>(jsonString, options)!;
}
