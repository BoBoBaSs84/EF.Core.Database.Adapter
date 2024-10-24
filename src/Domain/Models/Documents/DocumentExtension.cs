using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Documents;

/// <summary>
/// The document extension entity.
/// </summary>
public sealed class DocumentExtension : AuditedModel
{
	/// <summary>
	/// The name of the document extenion.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// The documents that use the extension.
	/// </summary>
	public required ICollection<Document> Documents { get; set; }
}
