using BB84.EntityFrameworkCore.Entities;

namespace BB84.Home.Domain.Entities.Documents;

/// <summary>
/// The document data entity.
/// </summary>
public sealed class DataEntity : AuditedEntity
{
	/// <summary>
	/// The <b>Message-Digest Algorithm 5</b> of the data.
	/// </summary>
	public required byte[] MD5Hash { get; set; }

	/// <summary>
	/// The length of the data content.
	/// </summary>
	public required long Length { get; set; }

	/// <summary>
	/// The actual content of the data.
	/// </summary>
	public required byte[] Content { get; set; }

	/// <summary>
	/// The navigational <see cref="Documents"/> property.
	/// </summary>
	public ICollection<DocumentEntity> Documents { get; } = [];
}
