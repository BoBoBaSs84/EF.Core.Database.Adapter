using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Documents;

/// <summary>
/// The document data entity.
/// </summary>
public sealed class DocumentData : AuditedModel
{
	/// <summary>
	/// The actual data of the document.
	/// </summary>
	public required byte[] Data { get; set; }

	/// <summary>
	/// The document identifier the data belongs to.
	/// </summary>
	public required Guid DocumentId { get; set; }

	/// <summary>
	/// The document the data belongs to.
	/// </summary>
	public required Document Document { get; set; }
}
