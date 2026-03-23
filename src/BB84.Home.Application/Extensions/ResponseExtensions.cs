using BB84.Extensions;
using BB84.Home.Application.Contracts.Responses.Todo;
using BB84.Home.Domain.Entities.Todo;

namespace BB84.Home.Application.Extensions;

/// <summary>
/// Response extensions for mapping domain entities to response DTOs.
/// </summary>
internal static class ResponseExtensions
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
			Items = [.. entity.Items.Select(i => new ItemResponse()
			{
				Id = i.Id,
				Title = i.Title,
				Priority = i.Priority,
				Reminder = i.Reminder,
				Done = i.Done,
				Note = i.Note
			})]
		};

		return response;
	}
}
