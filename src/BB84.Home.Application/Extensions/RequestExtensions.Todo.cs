using BB84.Extensions;
using BB84.Home.Application.Contracts.Requests.Todo;
using BB84.Home.Application.Contracts.Requests.Todo.Base;
using BB84.Home.Domain.Entities.Todo;

namespace BB84.Home.Application.Extensions;

internal static partial class RequestExtensions
{
	/// <summary>
	/// Converts a <see cref="ListBaseRequest"/> to a <see cref="ListEntity"/>.
	/// </summary>
	/// <param name="request">The request to convert.</param>
	/// <returns>The converted <see cref="ListEntity"/>.</returns>
	public static ListEntity ToEntity(this ListBaseRequest request)
	{
		ListEntity entity = new()
		{
			Title = request.Title,
			Color = request.Color?.FromRGBHexString()
		};

		return entity;
	}

	/// <summary>
	/// Converts a <see cref="ListBaseRequest"/> to a <see cref="ListEntity"/>.
	/// </summary>
	/// <param name="request">The request to convert.</param>
	/// <param name="userId">The ID of the user to which the list belongs.</param>
	/// <returns>The converted <see cref="ListEntity"/>.</returns>
	public static ListEntity ToEntity(this ListBaseRequest request, Guid userId)
	{
		ListEntity entity = request.ToEntity();
		entity.UserId = userId;

		return entity;
	}

	/// <summary>
	/// Converts a <see cref="ListUpdateRequest"/> to a <see cref="ListEntity"/>.
	/// </summary>
	/// <param name="request">The request to convert.</param>
	/// <param name="entity">The entity to update.</param>
	/// <returns>The converted <see cref="ListEntity"/>.</returns>
	public static ListEntity ToEntity(this ListUpdateRequest request, ListEntity entity)
	{
		entity.Title = request.Title;
		entity.Color = request.Color?.FromRGBHexString();

		return entity;
	}

	/// <summary>
	/// Converts an <see cref="ItemBaseRequest"/> to an <see cref="ItemEntity"/>.
	/// </summary>
	/// <param name="request">The request to convert.</param>
	/// <returns>The converted <see cref="ItemEntity"/>.</returns>
	public static ItemEntity ToEntity(this ItemBaseRequest request)
	{
		ItemEntity entity = new()
		{
			Title = request.Title,
			Priority = request.Priority,
			Reminder = request.Reminder,
			Note = request.Note
		};

		return entity;
	}

	/// <summary>
	/// Converts an <see cref="ItemBaseRequest"/> to an <see cref="ItemEntity"/>.
	/// </summary>
	/// <param name="request">The request to convert.</param>
	/// <param name="listId">The ID of the list to which the item belongs.</param>
	/// <returns>The converted <see cref="ItemEntity"/>.</returns>
	public static ItemEntity ToEntity(this ItemBaseRequest request, Guid listId)
	{
		ItemEntity entity = ToEntity(request);
		entity.ListId = listId;

		return entity;
	}

	/// <summary>
	/// Converts an <see cref="ItemUpdateRequest"/> to an <see cref="ItemEntity"/>.
	/// </summary>
	/// <param name="request">The request to convert.</param>
	/// <param name="entity">The entity to update.</param>
	/// <returns>The converted <see cref="ItemEntity"/>.</returns>
	public static ItemEntity ToEntity(this ItemUpdateRequest request, ItemEntity entity)
	{
		entity.Title = request.Title;
		entity.Priority = request.Priority;
		entity.Reminder = request.Reminder;
		entity.Note = request.Note;
		entity.Done = request.Done;

		return entity;
	}
}
