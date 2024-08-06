using System.ComponentModel.DataAnnotations;

using RESX = Domain.Properties.EnumeratorResources;

namespace Domain.Enumerators.Finance;

/// <summary>
/// The enumerator type for the bank account.
/// </summary>
public enum AccountType : byte
{
	/// <summary>
	/// The checking account type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AccountType_Checking_Name),
		ShortName = nameof(RESX.AccountType_Checking_ShortName),
		Description = nameof(RESX.AccountType_Checking_Description))]
	CHECKING = 1,
	/// <summary>
	/// The savings account type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AccountType_Savings_Name),
		ShortName = nameof(RESX.AccountType_Savings_ShortName),
		Description = nameof(RESX.AccountType_Savings_Description))]
	SAVINGS,
	/// <summary>
	/// The community account type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AccountType_Community_Name),
		ShortName = nameof(RESX.AccountType_Community_ShortName),
		Description = nameof(RESX.AccountType_Community_Description))]
	COMMUNITY
}
