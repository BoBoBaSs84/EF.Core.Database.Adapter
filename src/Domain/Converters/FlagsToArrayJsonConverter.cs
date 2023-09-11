using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Converters;

/// <summary>
/// The flags to array json converter class.
/// </summary>
/// <remarks>
/// Doues what it says, it converts enumerators decorated with the
/// <see cref="FlagsAttribute"/> to a string array.
/// </remarks>
public sealed class FlagsToArrayJsonConverter : JsonConverterFactory
{
	/// <inheritdoc/>
	public override bool CanConvert(Type typeToConvert)
		=> typeToConvert.IsEnum && typeToConvert.IsDefined(typeof(FlagsAttribute), false);

	/// <inheritdoc/>
	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		=> (JsonConverter)Activator.CreateInstance(typeof(EnumFlagsToStringArray<>).MakeGenericType(typeToConvert))!;
}

/// <summary>
/// The enum flags to string array class.
/// </summary>
/// <typeparam name="T">The enumerator type to use.</typeparam>
internal sealed class EnumFlagsToStringArray<T> : JsonConverter<T> where T : struct, Enum
{
	private readonly Dictionary<T, string> _enumToString = new();
	private readonly Dictionary<string, T> _stringToEnum = new();

	public EnumFlagsToStringArray()
	{
		Type type = typeof(T);
		Array values = Enum.GetValues(type);

		foreach (object? value in values)
		{
			MemberInfo info = type.GetMember($"{value}").First();
			EnumMemberAttribute? attribute = info.GetCustomAttributes(typeof(EnumMemberAttribute), false)
				.Cast<EnumMemberAttribute>()
				.FirstOrDefault();

			_stringToEnum.Add($"{value}", (T)value);

			if (attribute?.Value is not null)
			{
				_enumToString.Add((T)value, attribute.Value);
				_stringToEnum.Add(attribute.Value, (T)value);
			}
			else
			{
				_enumToString.Add((T)value, $"{value}");
			}
		}
	}

	/// <inheritdoc/>
	public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		switch (reader.TokenType)
		{
			case JsonTokenType.Null:
				return default;
			case JsonTokenType.StartArray:
				T value = default;
				while (reader.Read())
				{
					if (reader.TokenType == JsonTokenType.EndArray)
						break;

					string? stringValue = reader.GetString();

					if (stringValue is not null)
					{
						if (_stringToEnum.TryGetValue(stringValue, out T enumValue))
							value = Or(value, enumValue);
					}
				}
				return value;
			default:
				throw new JsonException();
		}
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		Array values = Enum.GetValues(value.GetType());
		writer.WriteStartArray();

		foreach (object? obj in values)
		{
			if (value.HasFlag((Enum)obj))
			{
				if (Convert.ToInt32(obj).Equals(0))
				{
					// handle "0" case which HasFlag matches to all values
					// --> only write "0" case if it is the only value present
					if (value.Equals(obj))
						writer.WriteStringValue(_enumToString[(T)obj]);
				}
				else
					writer.WriteStringValue(_enumToString[(T)obj]);
			}
		}
		writer.WriteEndArray();
	}

	private static T Or(T left, T right)
		=> Enum.GetUnderlyingType(left.GetType()) != typeof(long)
		? (T)Enum.ToObject(left.GetType(), Convert.ToInt64(left) | Convert.ToInt64(right))
		: (T)Enum.ToObject(left.GetType(), Convert.ToUInt64(left) | Convert.ToUInt64(right));
}
