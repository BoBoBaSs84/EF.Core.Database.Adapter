using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.Extensions;

/// <summary>
/// The enumerator extensions class.
/// </summary>
public static class EnumeratorExtensions
{
	/// <summary>
	/// Should get the description of an enumerator.
	/// </summary>
	/// <remarks>
	/// The <see cref="DisplayAttribute.GetDescription"/> method is used, so strings will be returned localized.
	/// </remarks>
	/// <typeparam name="TEnum">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute.Description"/> or or an empty string.</returns>
	public static string GetDescription<TEnum>(this TEnum enumValue) where TEnum : Enum
	{
		DisplayAttribute? attribute = enumValue.GetDisplayAttribute();
		return attribute is not null ? attribute.GetDescription() ?? string.Empty : string.Empty;
	}

	/// <summary>
	/// Should get the name of an enumerator.
	/// </summary>
	/// <remarks>
	/// The <see cref="DisplayAttribute.GetName"/> method is used, so strings will be returned localized.
	/// </remarks>
	/// <typeparam name="TEnum">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute.Name"/> or or an empty string.</returns>
	public static string GetName<TEnum>(this TEnum enumValue) where TEnum : Enum
	{
		DisplayAttribute? attribute = enumValue.GetDisplayAttribute();
		return attribute is not null ? attribute.GetName() ?? string.Empty : string.Empty;
	}

	/// <summary>
	/// Should get the short name of an enumerator.
	/// </summary>
	/// <remarks>
	/// The <see cref="DisplayAttribute.GetShortName"/> method is used, so strings will be returned localized.
	/// </remarks>
	/// <typeparam name="TEnum">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute.ShortName"/> or an empty string.</returns>
	public static string GetShortName<TEnum>(this TEnum enumValue) where TEnum : Enum
	{
		DisplayAttribute? attribute = enumValue.GetDisplayAttribute();
		return attribute is not null ? attribute.GetShortName() ?? string.Empty : string.Empty;
	}

	/// <summary>
	/// The <see cref="GetListFromEnum{T}(T)"/> method should return a list of all enumerators of the given type of enum.
	/// </summary>
	/// <typeparam name="TEnum">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>A list of all enums of the provided type.</returns>
	public static List<TEnum> GetListFromEnum<TEnum>(this TEnum enumValue) where TEnum : Enum =>
		Enum.GetValues(enumValue.GetType()).Cast<TEnum>().ToList();

	/// <summary>
	/// Should return the field metadata of an enumerator.
	/// </summary>
	/// <typeparam name="TEnum">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>The field metadata.</returns>
	public static FieldInfo GetFieldInfo<TEnum>(this TEnum enumValue) where TEnum : Enum =>
		enumValue.GetType().GetField(enumValue.ToString())!;

	/// <summary>
	/// Should get the display attribute of an enumerator.
	/// </summary>
	/// <remarks>
	/// Will return null if the enum is not decorated with the <see cref="DisplayAttribute"/>.
	/// </remarks>
	/// <typeparam name="TEnum">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute"/> or <see langword="null"/>.</returns>
	public static DisplayAttribute? GetDisplayAttribute<TEnum>(this TEnum enumValue) where TEnum : Enum
	{
		FieldInfo? fieldInfo = enumValue.GetFieldInfo();
		return fieldInfo is not null ? fieldInfo.GetCustomAttribute<DisplayAttribute>() : default;
	}
}
