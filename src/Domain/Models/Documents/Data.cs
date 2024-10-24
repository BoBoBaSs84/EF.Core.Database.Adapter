using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Documents;

/// <summary>
/// The document data entity.
/// </summary>
public sealed class Data : AuditedModel
{
	/// <summary>
	/// The actual data of the document.
	/// </summary>
	public required byte[] RawData { get; set; }

	/// <summary>
	/// The document identifier the data belongs to.
	/// </summary>
	public required Guid DocumentId { get; set; }

	/// <summary>
	/// The navigational <see cref="Document"/> property.
	/// </summary>
	public required Document Document { get; set; }
}
