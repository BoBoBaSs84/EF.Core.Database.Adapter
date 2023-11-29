using System.ComponentModel.DataAnnotations;

using Domain.Caches;
using Domain.Enumerators;

namespace Domain.Extensions;

/// <summary>
/// The enumerator extensions class.
/// </summary>
public static class EnumeratorExtensions
{
	/// <summary>
	/// Returns the description of the enumerator from the <see cref="DisplayAttributeCache{T}"/>.
	/// </summary>
	/// <remarks>
	/// The <see cref="DisplayAttribute.Description"/> property is used, so strings will be returned localized.
	/// </remarks>
	/// <typeparam name="T">The enmuerator itself.</typeparam>
	/// <param name="value">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute.Description"/> or the <paramref name="value"/> as string.</returns>
	public static string GetDescription<T>(this T value) where T : struct, IComparable, IFormattable, IConvertible
		=> DisplayAttributeCache<T>.GetDescription(value);

	/// <summary>
	/// Returns the name of the enumerator from the <see cref="DisplayAttributeCache{T}"/>.
	/// </summary>
	/// <remarks>
	/// The <see cref="DisplayAttribute.Name"/> method is used, so strings will be returned localized.
	/// </remarks>
	/// <typeparam name="T">The enmuerator itself.</typeparam>
	/// <param name="value">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute.Name"/> or the <paramref name="value"/> as string.</returns>
	public static string GetName<T>(this T value) where T : struct, IComparable, IFormattable, IConvertible
		=> DisplayAttributeCache<T>.GetName(value);

	/// <summary>
	/// Returns the short name of the enumerator from the <see cref="DisplayAttributeCache{T}"/>.
	/// </summary>
	/// <remarks>
	/// The <see cref="DisplayAttribute.ShortName"/> method is used, so strings will be returned localized.
	/// </remarks>
	/// <typeparam name="T">The enmuerator itself.</typeparam>
	/// <param name="value">The value of the enumerator.</param>
	/// <returns>The <see cref="DisplayAttribute.ShortName"/> or the <paramref name="value"/> as string.</returns>
	public static string GetShortName<T>(this T value) where T : struct, IComparable, IFormattable, IConvertible
		=> DisplayAttributeCache<T>.GetShortName(value);

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
