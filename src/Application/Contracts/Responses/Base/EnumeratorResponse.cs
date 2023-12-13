using System.ComponentModel.DataAnnotations;

using Domain.Extensions;

namespace Application.Contracts.Responses.Base;

/// <summary>
/// The <see langword="abstract"/> enumerator response class.
/// </summary>
/// <remarks>
/// Initilizes an instance of the enumerator response class.
/// </remarks>
/// <param name="enumValue">The enumerator value.</param>
public abstract class EnumeratorResponse<T>(T enumValue) where T : struct, IComparable, IFormattable, IConvertible
{
	/// <summary>
	/// The enumerator value.
	/// </summary>	
	public T Value { get; } = enumValue;

	/// <summary>
	/// The enumerator name.
	/// </summary>
	[DataType(DataType.Text)]
	public string Name { get; } = enumValue.GetName();

	/// <summary>
	/// The enumerator short name.
	/// </summary>
	[DataType(DataType.Text)]
	public string ShortName { get; } = enumValue.GetShortName();

	/// <summary>
	/// The enumerator description.
	/// </summary>
	[DataType(DataType.Text)]
	public string Description { get; } = enumValue.GetDescription();
}
