using Domain.Extensions;

namespace Application.Contracts.Responses.Base;

/// <summary>
/// The <see langword="abstract"/> enumerator response class.
/// </summary>
public abstract class EnumeratorResponse<T> where T : Enum
{
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
	}

	public T Value { get; }
	public string Name { get; }
	public string ShortName { get; }
	public string Description { get; }
}
