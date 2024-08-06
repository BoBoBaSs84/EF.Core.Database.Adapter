namespace Domain.Enumerators.Finance;

/// <summary>
/// The enumerator type for the bank account.
/// </summary>
public enum AccountType : byte
{
	/// <summary>
	/// The checking account type.
	/// </summary>
	CHECKING = 1,
	/// <summary>
	/// The savings account type.
	/// </summary>
	SAVINGS,
	/// <summary>
	/// The community account type.
	/// </summary>
	COMMUNITY
}
