namespace Domain.Interfaces.Models.Base;

/// <summary>
/// The identity model base interface.
/// </summary>
/// <typeparam name="TKey">The primary key type.</typeparam>
public interface IIdentityModelBase<TKey> where TKey : IEquatable<TKey>
{
	/// <summary>
	/// The key that uniquely identify each row of the table.
	/// </summary>
	TKey Id { get; }
}
