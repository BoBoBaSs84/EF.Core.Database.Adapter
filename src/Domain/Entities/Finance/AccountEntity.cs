using BB84.EntityFrameworkCore.Entities;
using BB84.Extensions;

using Domain.Enumerators.Finance;

namespace Domain.Entities.Finance;

/// <summary>
/// The account entity class.
/// </summary>
public sealed class AccountEntity : AuditedEntity
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
	public ICollection<AccountUserEntity> AccountUsers { get; set; } = default!;

	/// <summary>
	/// The <see cref="Transactions"/> property.
	/// </summary>
	public ICollection<AccountTransactionEntity> Transactions { get; set; } = default!;

	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public ICollection<CardEntity> Cards { get; set; } = default!;
}
