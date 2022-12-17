using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Database.Adapter.Entities.Extensions;

/// <summary>
/// 
/// </summary>
public static class EnumeratorExtensions
{
	/// <summary>
	/// The <see cref="GetEnumDescription{T}(T)"/> extension method will try to get the <see cref="DisplayAttribute.Description"/>.
	/// </summary>
	/// <remarks>
	/// The <see cref="DisplayAttribute.GetDescription"/> method is used, so strings will be returned localized.
	/// </remarks>
	/// <typeparam name="T">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute.Description"/> or the <paramref name="enumValue"/> as string.</returns>
	public static string GetEnumDescription<T>(this T enumValue) where T : Enum
	{
		DisplayAttribute? attribute = enumValue.GetDisplayAttribute();
		return attribute is not null ? attribute.GetDescription() ?? enumValue.ToString() : enumValue.ToString();
	}

	/// <summary>
	/// The <see cref="GetEnumName{T}(T)"/> extension method will try to get the <see cref="DisplayAttribute.Name"/>.
	/// </summary>
	/// <remarks>
	/// The <see cref="DisplayAttribute.GetName"/> method is used, so strings will be returned localized.
	/// </remarks>
	/// <typeparam name="T">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute.Name"/> or the <paramref name="enumValue"/> as string.</returns>
	public static string GetEnumName<T>(this T enumValue) where T : Enum
	{
		DisplayAttribute? attribute = enumValue.GetDisplayAttribute();
		return attribute is not null ? attribute.GetName() ?? enumValue.ToString() : enumValue.ToString();
	}

	/// <summary>
	/// The <see cref="GetEnumShortName{T}(T)"/> extension method will try to get the <see cref="DisplayAttribute.ShortName"/>.
	/// </summary>
	/// <remarks>
	/// The <see cref="DisplayAttribute.GetShortName"/> method is used, so strings will be returned localized.
	/// </remarks>
	/// <typeparam name="T">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute.ShortName"/> or the <paramref name="enumValue"/> as string.</returns>
	public static string GetEnumShortName<T>(this T enumValue) where T : Enum
	{
		DisplayAttribute? attribute = enumValue.GetDisplayAttribute();
		return attribute is not null ? attribute.GetShortName() ?? enumValue.ToString() : enumValue.ToString();
	}

	/// <summary>
	/// The <see cref="GetDisplayAttribute{T}(T)"/> method should return the <see cref="DisplayAttribute"/> from the enum.
	/// </summary>
	/// <remarks>
	/// Will return null if the enum is not decorated with the <see cref="DisplayAttribute"/>.
	/// </remarks>
	/// <typeparam name="T">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute"/> or <see langword="null"/>.</returns>
	private static DisplayAttribute? GetDisplayAttribute<T>(this T enumValue) where T : Enum
	{
		FieldInfo? fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
		return fieldInfo is not null ? fieldInfo.GetCustomAttribute<DisplayAttribute>() : default;
	}

	/// <summary>
	/// The <see cref="GetListFromEnum{T}(T)"/> method should return a list of all enumerators of the given type of enum.
	/// </summary>
	/// <typeparam name="T">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>A list of all enums of the provided type.</returns>
	public static List<T> GetListFromEnum<T>(this T enumValue) where T : Enum
		=> Enum.GetValues(enumValue.GetType()).Cast<T>().ToList();
}
