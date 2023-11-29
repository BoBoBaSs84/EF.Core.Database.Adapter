using System.ComponentModel.DataAnnotations;

using Domain.Caches;

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
}
