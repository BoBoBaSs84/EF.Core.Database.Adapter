namespace DA.Domain.Models.BaseTypes.Interfaces;

/// <summary>
/// The identity model interface.
/// </summary>
/// <typeparam name="TKey">The primary key type.</typeparam>
internal interface IIdentityModel<TKey> where TKey : IEquatable<TKey>
{
	/// <summary>
	/// The <see cref="Id"/> property.
	/// </summary>
	/// <remarks>
	/// This is the primary key of the database table.
	/// </remarks>
	TKey Id { get; }
}
