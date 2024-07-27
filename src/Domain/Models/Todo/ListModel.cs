using System.Drawing;

using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Todo;

/// <summary>
/// The todo list model class.
/// </summary>
public class TodoListModel : AuditedModel
{
	/// <summary>
	/// The title of the todo list.
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	/// The color of the todo list.
	/// </summary>
	public Color? Color { get; set; }

	/// <summary>
	/// The items within the todo list.
	/// </summary>
	public virtual ICollection<ItemModel> Items { get; private set; } = [];

	/// <summary>
	/// The navigational <see cref="Users"/> property.
	/// </summary>
	public virtual ICollection<ListUserModel> Users { get; private set; } = [];
}
