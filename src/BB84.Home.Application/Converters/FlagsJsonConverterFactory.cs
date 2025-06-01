using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BB84.Home.Application.Converters;

/// <summary>
/// Represents a JSON converter factory for enum types with the <see cref="FlagsAttribute"/>.
/// </summary>
public sealed class FlagsJsonConverterFactory : JsonConverterFactory
{
	/// <inheritdoc/>
	public override bool CanConvert(Type typeToConvert)
		=> typeToConvert.IsEnum && typeToConvert.IsDefined(typeof(FlagsAttribute), false);

	/// <inheritdoc/>
	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		Type converterType = typeof(EnumWithFlagsJsonConverter<>).MakeGenericType(typeToConvert);
		return (JsonConverter)Activator.CreateInstance(converterType)!;
	}
}

/// <summary>
/// Provides custom JSON serialization and deserialization for enums with the <see cref="FlagsAttribute"/>.
/// </summary>
/// <remarks>
/// This converter supports enums marked with the <see cref="FlagsAttribute"/> and handles
/// serialization and deserialization of individual flag values or combinations of flags.
/// It uses the <see cref="EnumMemberAttribute"/> to map enum values to their corresponding
/// string representations in JSON.
/// <para>During deserialization, the converter can parse JSON arrays containing multiple
/// flag values and combine them into a single enum value. During serialization, it outputs
/// a JSON array of string representations for the active flags.</para>
/// </remarks>
/// <typeparam name="TEnum">
/// The type of the enum to be serialized or deserialized. Must be a struct and an enum.
/// </typeparam>
public class EnumWithFlagsJsonConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
	private readonly Dictionary<TEnum, string> _enumToString = [];
	private readonly Dictionary<string, TEnum> _stringToEnum = [];

	/// <summary>
	/// Initializes a new instance of the <see cref="EnumWithFlagsJsonConverter{TEnum}"/> class.
	/// </summary>
	public EnumWithFlagsJsonConverter()
	{
		Type type = typeof(TEnum);
		TEnum[] values = Enum.GetValues<TEnum>();

		foreach (TEnum value in values)
		{
			MemberInfo enumMember = type.GetMember(value.ToString())[0];
			EnumMemberAttribute? attr = enumMember.GetCustomAttributes(typeof(EnumMemberAttribute), false)
				.OfType<EnumMemberAttribute>()
				.FirstOrDefault();

			_stringToEnum.Add(value.ToString(), value);

			if (attr?.Value != null)
			{
				_enumToString.Add(value, attr.Value);
				_stringToEnum.Add(attr.Value, value);
			}
			else
			{
				_enumToString.Add(value, value.ToString());
			}
		}
	}

	/// <inheritdoc/>
	public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		switch (reader.TokenType)
		{
			case JsonTokenType.Null:
				return default(TEnum);
			case JsonTokenType.StartArray:
				TEnum ret = default(TEnum);
				while (reader.Read())
				{
					if (reader.TokenType == JsonTokenType.EndArray)
						break;
					string? stringValue = reader.GetString();
					if (_stringToEnum.TryGetValue(stringValue, out TEnum _enumValue))
					{
						ret = Or(ret, _enumValue);
					}
				}
				return ret;
			default:
				throw new JsonException();
		}
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
	{
		TEnum[] values = Enum.GetValues<TEnum>();
		writer.WriteStartArray();
		foreach (TEnum _value in values)
		{
			if (value.HasFlag(_value))
			{
				int v = Convert.ToInt32(_value);
				if (v == 0)
					if (value.Equals(_value))
						writer.WriteStringValue(_enumToString[_value]);
					else
						writer.WriteStringValue(_enumToString[_value]);
			}
		}
		writer.WriteEndArray();
	}

	/// <summary>
	/// Performs a bitwise OR operation on two enumeration values.
	/// </summary>
	/// <remarks>
	/// This method assumes that the underlying type of the enumeration is either
	/// an integral type (e.g., <see langword="int"/> or <see langword="ulong"/>).
	/// If the underlying type is not supported, an exception may be thrown during execution.
	/// </remarks>
	/// <param name="first">The first enumeration value.</param>
	/// <param name="second">The second enumeration value.</param>
	/// <returns>
	/// A value of type <typeparamref name="TEnum"/> that represents the result of the bitwise
	/// OR operation between <paramref name="first"/> and <paramref name="second"/>.
	/// </returns>
	private static TEnum Or(TEnum first, TEnum second)
		=> Enum.GetUnderlyingType(first.GetType()) != typeof(ulong)
			? (TEnum)Enum.ToObject(first.GetType(), Convert.ToInt64(first) | Convert.ToInt64(second))
			: (TEnum)Enum.ToObject(first.GetType(), Convert.ToUInt64(first) | Convert.ToUInt64(second));
}
