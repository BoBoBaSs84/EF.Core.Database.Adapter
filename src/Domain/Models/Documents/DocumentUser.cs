using BB84.EntityFrameworkCore.Models;

using Domain.Models.Identity;

namespace Domain.Models.Documents;

/// <summary>
/// The document to user entity.
/// </summary>
public sealed class DocumentUser : AuditedModel
{
	/// <summary>
	/// The <see cref="DocumentId"/> property.
	/// </summary>
	public Guid DocumentId { get; set; }

	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The <see cref="Document"/> property.
	/// </summary>
	public required Document Document { get; set; }

	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public required UserModel User { get; set; }
}
