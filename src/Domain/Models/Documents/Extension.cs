using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Documents;

/// <summary>
/// The document extension entity.
/// </summary>
public sealed class Extension : AuditedModel
{
	/// <summary>
	/// The name of the document extenion.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// The mime type of the document extenion.
	/// </summary>
	public string? MimeType { get; set; }

	/// <summary>
	/// The navigational <see cref="Documents"/> property.
	/// </summary>
	public ICollection<Document> Documents { get; set; } = default!;
}
