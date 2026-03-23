using BB84.Extensions;
using BB84.Home.Application.Contracts.Responses.Todo;
using BB84.Home.Domain.Entities.Todo;

namespace BB84.Home.Application.Extensions;

internal static partial class ResponseExtensions
{
	/// <summary>
	/// Converts a <see cref="ListEntity"/> instance to its corresponding <see cref="ListResponse"/>
	/// representation.
	/// </summary>
	/// <param name="entity">The <see cref="ListEntity"/> to convert.</param>
	/// <returns>
	/// A <see cref="ListResponse"/> object containing the data from the specified <see cref="ListEntity"/>.
	/// </returns>
	public static ListResponse ToResponse(this ListEntity entity)
	{
		ListResponse response = new()
		{
			Id = entity.Id,
			Title = entity.Title,
			Color = entity.Color?.ToRGBHexString(),
			Items = [.. entity.Items.Select(ToResponse)]
		};

		return response;
	}

	/// <summary>
	/// Converts a <see cref="ItemEntity"/> instance to its corresponding <see cref="ItemResponse"/>
	/// representation.
	/// </summary>
	/// <param name="entity">The <see cref="ItemEntity"/> to convert.</param>
	/// <returns>
	/// A <see cref="ItemResponse"/> object containing the data from the specified <see cref="ItemEntity"/>.
	/// </returns>
	public static ItemResponse ToResponse(this ItemEntity entity)
	{
		ItemResponse response = new()
		{
			Id = entity.Id,
			Title = entity.Title,
			Priority = entity.Priority,
			Reminder = entity.Reminder,
			Done = entity.Done,
			Note = entity.Note
		};
		return response;
	}
}
