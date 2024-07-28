using BB84.EntityFrameworkCore.Models;

using Domain.Models.Identity;

namespace Domain.Models.Todo;

/// <summary>
/// The todo list user class.
/// </summary>
public sealed class ListUser : AuditedCompositeModel
{
	/// <summary>
	/// The <see cref="ListId"/> property.
	/// </summary>
	public Guid ListId { get; set; }

	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The <see cref="TodoList"/> property.
	/// </summary>
	public List TodoList { get; set; } = default!;

	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public UserModel User { get; set; } = default!;
}
