using BB84.Home.Application.Contracts.Requests.Documents;
using BB84.Home.Domain.Entities.Documents;

namespace BB84.Home.Application.Extensions;

internal static partial class RequestExtensions
{
	/// <summary>
	/// Converts a <see cref="DocumentCreateRequest"/> to a <see cref="DocumentEntity"/>.
	/// </summary>
	/// <param name="request">The request to convert.</param>
	/// <returns>The converted <see cref="DocumentEntity"/>.</returns>
	public static DocumentEntity ToEntity(this DocumentCreateRequest request)
	{
		DocumentEntity entity = new()
		{
			Name = request.Name,
			Directory = request.Directory,
			Flags = request.Flags,
			CreationTime = request.CreationTime,
			LastWriteTime = request.LastWriteTime,
			LastAccessTime = request.LastAccessTime
		};

		return entity;
	}

	/// <summary>
	/// Converts a <see cref="DocumentUpdateRequest"/> to a <see cref="DocumentEntity"/>.
	/// </summary>
	/// <param name="request">The request to convert.</param>
	/// <param name="entity">The entity to update.</param>
	/// <returns>The updated <see cref="DocumentEntity"/>.</returns>
	public static DocumentEntity ToEntity(this DocumentUpdateRequest request, DocumentEntity entity)
	{
		entity.Name = request.Name;
		entity.Directory = request.Directory;
		entity.Flags = request.Flags;
		entity.CreationTime = request.CreationTime;
		entity.LastWriteTime = request.LastWriteTime;
		entity.LastAccessTime = request.LastAccessTime;

		return entity;
	}
}
