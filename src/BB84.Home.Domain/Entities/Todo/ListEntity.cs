using System.Drawing;

using BB84.EntityFrameworkCore.Entities;
using BB84.Home.Domain.Entities.Identity;

namespace BB84.Home.Domain.Entities.Todo;

/// <summary>
/// The todo list entity class.
/// </summary>
public class ListEntity : AuditedEntity
{
	/// <summary>
	/// The title of the todo list.
	/// </summary>
	public string Title { get; set; } = default!;

	/// <summary>
	/// The color of the todo list.
	/// </summary>
	public Color? Color { get; set; }

	/// <summary>
	/// The identifier of the user.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The items within the todo list.
	/// </summary>
	public virtual ICollection<ItemEntity> Items { get; set; } = default!;

	/// <summary>
	/// The navigational <see cref="User"/> property.
	/// </summary>
	public virtual UserEntity User { get; set; } = default!;
}
