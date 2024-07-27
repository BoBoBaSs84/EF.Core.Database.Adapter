using BB84.EntityFrameworkCore.Models;

using Domain.Models.Identity;

namespace Domain.Models.Todo;

/// <summary>
/// The todo list user model class.
/// </summary>
public class ListUserModel : AuditedCompositeModel
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
	public virtual TodoListModel TodoList { get; private set; } = default!;

	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual UserModel User { get; private set; } = default!;
}
