using BB84.EntityFrameworkCore.Models;

using Domain.Models.Identity;

namespace Domain.Models.Documents;

/// <summary>
/// The document to user entity.
/// </summary>
public sealed class DocumentUser : AuditedCompositeModel
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
	/// The navigational <see cref="Document"/> property.
	/// </summary>
	public Document Document { get; set; } = default!;

	/// <summary>
	/// The navigational <see cref="User"/> property.
	/// </summary>
	public UserModel User { get; set; } = default!;
}
