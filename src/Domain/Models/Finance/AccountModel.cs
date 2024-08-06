using BB84.EntityFrameworkCore.Models;
using BB84.Extensions;

using Domain.Enumerators.Finance;

namespace Domain.Models.Finance;

/// <summary>
/// The account model class.
/// </summary>
public sealed class AccountModel : AuditedModel
{
	private string _iban = default!;

	/// <summary>
	/// The <see cref="IBAN"/> property.
	/// </summary>	
	public string IBAN
	{
		get => _iban;
		set
		{
			if (value != _iban)
				_iban = value.RemoveWhitespace();
		}
	}

	/// <summary>
	/// The <see cref="Type"/> property.
	/// </summary>
	public AccountType Type { get; set; }

	/// <summary>
	/// The <see cref="Provider"/> property.
	/// </summary>
	public string Provider { get; set; } = default!;

	/// <summary>
	/// The <see cref="AccountUsers"/> property.
	/// </summary>
	public ICollection<AccountUserModel> AccountUsers { get; set; } = [];

	/// <summary>
	/// The <see cref="Transactions"/> property.
	/// </summary>
	public ICollection<AccountTransactionModel> Transactions { get; set; } = [];

	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public ICollection<CardModel> Cards { get; set; } = [];
}
