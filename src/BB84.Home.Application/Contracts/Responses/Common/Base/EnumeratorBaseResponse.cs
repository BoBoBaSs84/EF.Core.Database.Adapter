using System.ComponentModel.DataAnnotations;

using BB84.Home.Domain.Extensions;

namespace BB84.Home.Application.Contracts.Responses.Common.Base;

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
	public T Value => enumValue;

	/// <summary>
	/// The name of the enumerator.
	/// </summary>
	[DataType(DataType.Text)]
	public string Name => enumValue.GetName();

	/// <summary>
	/// The short name of the enumerator.
	/// </summary>
	[DataType(DataType.Text)]
	public string ShortName => enumValue.GetShortName();

	/// <summary>
	/// The description of the enumerator.
	/// </summary>
	[DataType(DataType.Text)]
	public string Description => enumValue.GetDescription();
}
