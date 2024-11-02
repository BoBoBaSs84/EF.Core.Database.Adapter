using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Documents;

/// <summary>
/// The document data entity.
/// </summary>
public sealed class Data : AuditedModel
{
	/// <summary>
	/// The <b>Message-Digest Algorithm 5</b> of the data.
	/// </summary>
	public byte[] MD5Hash { get; set; } = default!;

	/// <summary>
	/// The length of the data content.
	/// </summary>
	public long Length { get; set; } = default!;

	/// <summary>
	/// The actual content of the data.
	/// </summary>
	public byte[] Content { get; set; } = default!;

	/// <summary>
	/// The navigational <see cref="Documents"/> property.
	/// </summary>
	public ICollection<Document> Documents { get; set; } = default!;
}
