using System.ComponentModel.DataAnnotations;
using System.Reflection;

using Domain.Enumerators;

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
	public static string GetDescription<TEnum>(this TEnum enumValue) where TEnum : struct, IComparable, IFormattable, IConvertible
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
	public static string GetName<TEnum>(this TEnum enumValue) where TEnum : struct, IComparable, IFormattable, IConvertible
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
	public static string GetShortName<TEnum>(this TEnum enumValue) where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		DisplayAttribute? attribute = enumValue.GetDisplayAttribute();
		return attribute is not null ? attribute.GetShortName() ?? string.Empty : string.Empty;
	}

	/// <summary>
	/// Should return the field metadata of an enumerator.
	/// </summary>
	/// <typeparam name="TEnum">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>The field metadata.</returns>
	public static FieldInfo GetFieldInfo<TEnum>(this TEnum enumValue) where TEnum : struct, IComparable, IFormattable, IConvertible
		=> enumValue.GetType().GetField(enumValue.ToString())!;

	/// <summary>
	/// Should get the display attribute of an enumerator.
	/// </summary>
	/// <remarks>
	/// Will return null if the enum is not decorated with the <see cref="DisplayAttribute"/>.
	/// </remarks>
	/// <typeparam name="TEnum">The enmuerator itself.</typeparam>
	/// <param name="enumValue">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute"/> or <see langword="null"/>.</returns>
	public static DisplayAttribute? GetDisplayAttribute<TEnum>(this TEnum enumValue) where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		FieldInfo? fieldInfo = enumValue.GetFieldInfo();
		return fieldInfo is not null ? fieldInfo.GetCustomAttribute<DisplayAttribute>() : default;
	}

	/// <summary>
	/// Returns if the attendance type is working hours relevant.
	/// </summary>
	/// <param name="type">The attendance type enumerator to work with.</param>
	/// <returns>
	/// <see langword="true"/> if the attendance type is working hours relevant, otherwise <see langword="false"/>
	/// </returns>
	public static bool IsWorkingHoursRelevant(this AttendanceType type)
		=> type switch
		{
			AttendanceType.WORKDAY => true,
			AttendanceType.ABSENCE => true,
			AttendanceType.BUISNESSTRIP => true,
			AttendanceType.MOBILEWORKING => true,
			AttendanceType.SHORTTIMEWORK => true,
			AttendanceType.VACATIONBLOCK => true,
			AttendanceType.PLANNEDVACATION => true,
			_ => false,
		};
}
