using BB84.EntityFrameworkCore.Entities;

namespace BB84.Home.Domain.Entities.Finance;

/// <summary>
/// The transaction entity class.
/// </summary>
public sealed class TransactionEntity : AuditedEntity
{
	/// <summary>
	/// The <see cref="BookingDate"/> property.
	/// </summary>
	public DateTime BookingDate { get; set; }

	/// <summary>
	/// The <see cref="ValueDate"/> property.
	/// </summary>
	public DateTime? ValueDate { get; set; }

	/// <summary>
	/// The <see cref="PostingText"/> property.
	/// </summary>
	public string PostingText { get; set; } = default!;

	/// <summary>
	/// The <see cref="ClientBeneficiary"/> property.
	/// </summary>
	public string ClientBeneficiary { get; set; } = default!;

	/// <summary>
	/// The <see cref="Purpose"/> property.
	/// </summary>
	public string? Purpose { get; set; }

	/// <summary>
	/// The <see cref="AccountNumber"/> property.
	/// </summary>
	public string AccountNumber { get; set; } = default!;

	/// <summary>
	/// The <see cref="BankCode"/> property.
	/// </summary>
	public string BankCode { get; set; } = default!;

	/// <summary>
	/// The <see cref="AmountEur"/> property.
	/// </summary>
	public decimal AmountEur { get; set; } = default!;

	/// <summary>
	/// The <see cref="CreditorId"/> property.
	/// </summary>
	public string? CreditorId { get; set; }

	/// <summary>
	/// The <see cref="MandateReference"/> property.
	/// </summary>
	public string? MandateReference { get; set; }

	/// <summary>
	/// The <see cref="CustomerReference"/> property.
	/// </summary>
	public string? CustomerReference { get; set; }

	/// <summary>
	/// The <see cref="AccountTransactions"/> property.
	/// </summary>
	public ICollection<AccountTransactionEntity> AccountTransactions { get; set; } = default!;

	/// <summary>
	/// The <see cref="CardTransactions"/> property.
	/// </summary>
	public ICollection<CardTransactionEntity> CardTransactions { get; set; } = default!;
}
