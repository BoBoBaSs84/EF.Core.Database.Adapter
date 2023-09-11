using System.ComponentModel.DataAnnotations;

using Domain.Extensions;

namespace Application.Contracts.Responses.Base;

/// <summary>
/// The <see langword="abstract"/> enumerator response class.
/// </summary>
public abstract class EnumeratorResponse<T> where T : Enum
{
	private readonly Type _type;
	/// <summary>
	/// Initilizes an instance of the enumerator response class.
	/// </summary>
	/// <param name="enumValue">The enumerator value.</param>
	public EnumeratorResponse(T enumValue)
	{
		Value = enumValue;
		Name = enumValue.GetName();
		ShortName = enumValue.GetShortName();
		Description = enumValue.GetDescription();
		_type = enumValue.GetType();
	}

	/// <summary>
	/// The enumerator value.
	/// </summary>
	public T Value { get; }

	/// <summary>
	/// The enumerator name.
	/// </summary>
	[DataType(DataType.Text)]
	public string Name { get; }

	/// <summary>
	/// The enumerator short name.
	/// </summary>
	[DataType(DataType.Text)]
	public string ShortName { get; }

	/// <summary>
	/// The enumerator description.
	/// </summary>
	[DataType(DataType.Text)]
	public string Description { get; }
}
