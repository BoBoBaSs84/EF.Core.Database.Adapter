using System.ComponentModel.DataAnnotations;

using RESX = BB84.Home.Domain.Properties.EnumeratorResources;

namespace BB84.Home.Domain.Enumerators.Documents;

/// <summary>
/// The document flags enumerator.
/// </summary>
[Flags]
public enum DocumentTypes : long
{
	/// <summary>
	/// The document has no flag.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DocumentTypes_None_Name),
		ShortName = nameof(RESX.DocumentTypes_None_ShortName),
		Description = nameof(RESX.DocumentTypes_None_Description))]
	None = 0,

	/// <summary>
	/// The document is relevant for invoices.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DocumentTypes_Invoice_Name),
		ShortName = nameof(RESX.DocumentTypes_Invoice_ShortName),
		Description = nameof(RESX.DocumentTypes_Invoice_Description))]
	Invoice = 1 << 1,

	/// <summary>
	/// The document is relevant for education.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DocumentTypes_Eductation_Name),
		ShortName = nameof(RESX.DocumentTypes_Eductation_ShortName),
		Description = nameof(RESX.DocumentTypes_Eductation_Description))]
	Eductation = 1 << 2,

	/// <summary>
	/// The document is relevant for certification.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DocumentTypes_Certificate_Name),
		ShortName = nameof(RESX.DocumentTypes_Certificate_ShortName),
		Description = nameof(RESX.DocumentTypes_Certificate_Description))]
	Certificate = 1 << 3,

	/// <summary>
	/// The document is relevant for the healthcare system.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DocumentTypes_Healthcare_Name),
		ShortName = nameof(RESX.DocumentTypes_Healthcare_ShortName),
		Description = nameof(RESX.DocumentTypes_Healthcare_Description))]
	Healthcare = 1 << 4,

	/// <summary>
	/// The document is relevant for tax purposes.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.DocumentTypes_Tax_Name),
		ShortName = nameof(RESX.DocumentTypes_Tax_ShortName),
		Description = nameof(RESX.DocumentTypes_Tax_Description))]
	Tax = 1 << 5,
}
