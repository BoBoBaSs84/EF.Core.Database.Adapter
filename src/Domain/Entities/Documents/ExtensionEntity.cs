using BB84.EntityFrameworkCore.Entities;

namespace Domain.Entities.Documents;

/// <summary>
/// The document extension entity.
/// </summary>
public sealed class ExtensionEntity : AuditedEntity
{
	/// <summary>
	/// The name of the document extenion.
	/// </summary>
	public string Name { get; set; } = default!;

	/// <summary>
	/// The mime type of the document extenion.
	/// </summary>
	public string? MimeType { get; set; }

	/// <summary>
	/// The navigational <see cref="Documents"/> property.
	/// </summary>
	public ICollection<DocumentEntity> Documents { get; set; } = default!;
}
