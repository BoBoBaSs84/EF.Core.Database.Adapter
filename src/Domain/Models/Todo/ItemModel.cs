using BB84.EntityFrameworkCore.Models;

using Domain.Enumerators;

namespace Domain.Models.Todo;

/// <summary>
/// The todo item model class.
/// </summary>
public sealed class ItemModel : AuditedModel
{
	/// <summary>
	/// The title of the todo item.
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	/// The note on the todo item.
	/// </summary>
	public string? Note { get; set; }

	/// <summary>
	/// The priority of the todo item.
	/// </summary>
	public PriorityLevelType Priority { get; set; }

	/// <summary>
	/// The remind date of the todo item.
	/// </summary>
	public DateTime? Reminder { get; set; }

	/// <summary>
	/// Indicates if the todo item is done.
	/// </summary>
	public bool Done { get; set; }

	/// <summary>
	/// The identifier of the todo list object.
	/// </summary>
	public Guid ListId { get; set; }

	/// <summary>
	/// The navigational reference to the todo list object.
	/// </summary>
	public TodoListModel List { get; set; } = null!;
}
