using BB84.EntityFrameworkCore.Entities;

namespace BB84.Home.Domain.Entities.Documents;

/// <summary>
/// The document extension entity.
/// </summary>
public sealed class ExtensionEntity : AuditedEntity
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
	public ICollection<DocumentEntity> Documents { get; } = [];
}
