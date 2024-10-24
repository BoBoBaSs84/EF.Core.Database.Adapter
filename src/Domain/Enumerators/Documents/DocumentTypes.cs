namespace Domain.Enumerators.Documents;

/// <summary>
/// The document flags enumerator.
/// </summary>
[Flags]
public enum DocumentTypes : long
{
	/// <summary>
	/// The document has no flag.
	/// </summary>
	None = 0,
	Invoice = 1 << 1,
	Eductation = 1 << 2,
	Certificate = 1 << 3,
	Healthcare = 1 << 4,
	TaxDeclaration = 1 << 5,
}
