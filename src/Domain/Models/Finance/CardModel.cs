using BB84.EntityFrameworkCore.Models;
using BB84.Extensions;

using Domain.Enumerators;
using Domain.Models.Identity;

namespace Domain.Models.Finance;

/// <summary>
/// The card model class.
/// </summary>
public sealed class CardModel : AuditedModel
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
	public CardType CardType { get; set; }

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
	public UserModel User { get; set; } = default!;

	/// <summary>
	/// The <see cref="Account"/> property.
	/// </summary>
	public AccountModel Account { get; set; } = default!;

	/// <summary>
	/// The <see cref="Transactions"/> property.
	/// </summary>
	public ICollection<CardTransactionModel> Transactions { get; set; } = [];
}
