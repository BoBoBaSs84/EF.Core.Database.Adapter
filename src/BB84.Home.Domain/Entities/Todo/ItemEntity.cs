using BB84.EntityFrameworkCore.Entities;
using BB84.Home.Domain.Enumerators.Todo;

namespace BB84.Home.Domain.Entities.Todo;

/// <summary>
/// The todo item entity class.
/// </summary>
public sealed class ItemEntity : AuditedEntity
{
	/// <summary>
	/// The identifier of the todo list object.
	/// </summary>
	public Guid ListId { get; set; }

	/// <summary>
	/// The title of the todo item.
	/// </summary>
	public string Title { get; set; } = default!;

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
	/// The navigational reference to the todo list object.
	/// </summary>
	public ListEntity List { get; set; } = default!;
}
