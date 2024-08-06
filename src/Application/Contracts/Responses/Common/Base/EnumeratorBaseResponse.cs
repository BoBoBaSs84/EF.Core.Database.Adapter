using System.ComponentModel.DataAnnotations;

using Domain.Extensions;

namespace Application.Contracts.Responses.Common.Base;

/// <summary>
/// The base class for enumerator responses.
/// </summary>
/// <typeparam name="T">The enumerator type to work with.</typeparam>
/// <param name="enumValue">The enumerator value.</param>
public abstract class EnumeratorBaseResponse<T>(T enumValue) where T : struct, IComparable, IFormattable, IConvertible
{
	/// <summary>
	/// The enumerator value.
	/// </summary>	
	public T Value { get; } = enumValue;

	/// <summary>
	/// The name of the enumerator.
	/// </summary>
	[DataType(DataType.Text)]
	public string Name { get; } = enumValue.GetName();

	/// <summary>
	/// The short name of the enumerator.
	/// </summary>
	[DataType(DataType.Text)]
	public string ShortName { get; } = enumValue.GetShortName();

	/// <summary>
	/// The description of the enumerator.
	/// </summary>
	[DataType(DataType.Text)]
	public string Description { get; } = enumValue.GetDescription();
}
