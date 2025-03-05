using BB84.EntityFrameworkCore.Entities;
using BB84.Extensions;
using BB84.Home.Domain.Entities.Identity;
using BB84.Home.Domain.Enumerators.Finance;

namespace BB84.Home.Domain.Entities.Finance;

/// <summary>
/// The card entity class.
/// </summary>
public sealed class CardEntity : AuditedEntity
{
	private string _pan = default!;

	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The <see cref="AccountId"/> property.
	/// </summary>
	public Guid AccountId { get; set; }

	/// <summary>
	/// The card type property.
	/// </summary>
	public CardType Type { get; set; }

	/// <summary>
	/// The <see cref="PAN"/> property.
	/// </summary>
	/// <remarks>
	/// The payment card number or <b>p</b>rimary <b>a</b>ccount <b>n</b>umber.
	/// </remarks>
	public string PAN
	{
		get => _pan;
		set
		{
			if (value != _pan)
				_pan = value.RemoveWhitespace();
		}
	}

	/// <summary>
	/// The <see cref="ValidUntil"/> property.
	/// </summary>
	public DateTime ValidUntil { get; set; }

	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public UserEntity User { get; set; } = default!;

	/// <summary>
	/// The <see cref="Account"/> property.
	/// </summary>
	public AccountEntity Account { get; set; } = default!;

	/// <summary>
	/// The <see cref="Transactions"/> property.
	/// </summary>
	public ICollection<CardTransactionEntity> Transactions { get; set; } = default!;
}
