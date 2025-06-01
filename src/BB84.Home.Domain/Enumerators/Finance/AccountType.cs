using System.ComponentModel.DataAnnotations;

using RESX = BB84.Home.Domain.Properties.EnumeratorResources;

namespace BB84.Home.Domain.Enumerators.Finance;

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
	Checking = 1,
	/// <summary>
	/// The savings account type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AccountType_Savings_Name),
		ShortName = nameof(RESX.AccountType_Savings_ShortName),
		Description = nameof(RESX.AccountType_Savings_Description))]
	Savings,
	/// <summary>
	/// The community account type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.AccountType_Community_Name),
		ShortName = nameof(RESX.AccountType_Community_ShortName),
		Description = nameof(RESX.AccountType_Community_Description))]
	Community
}
