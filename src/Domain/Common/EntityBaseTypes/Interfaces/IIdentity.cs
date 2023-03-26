namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The identity model interface.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IEquatable{T}"/> interface.
/// </remarks>
/// <typeparam name="TKey">The primary key type.</typeparam>
public interface IIdentity<TKey> where TKey : IEquatable<TKey>
{
	/// <summary>
	/// The identifier property.
	/// </summary>
	/// <remarks>
	/// This is the primary key of the database table of type <typeparamref name="TKey"/>.
	/// </remarks>
	TKey Id { get; }
}
