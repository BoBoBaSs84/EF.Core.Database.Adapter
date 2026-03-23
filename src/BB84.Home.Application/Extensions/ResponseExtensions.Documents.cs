using BB84.Home.Application.Contracts.Responses.Documents;
using BB84.Home.Domain.Entities.Documents;

namespace BB84.Home.Application.Extensions;

internal static partial class ResponseExtensions
{
	/// <summary>
	/// Converts a <see cref="DocumentEntity"/> to a <see cref="DocumentResponse"/>.
	/// </summary>
	/// <param name="entity">The <see cref="DocumentEntity"/> to convert.</param>
	/// <returns>The converted <see cref="DocumentResponse"/>.</returns>
	public static DocumentResponse ToResponse(this DocumentEntity entity)
	{
		DocumentResponse response = new()
		{
			Id = entity.Id,
			Name = entity.Name,
			Directory = entity.Directory,
			Flags = entity.Flags,
			CreationTime = entity.CreationTime,
			LastWriteTime = entity.LastWriteTime,
			LastAccessTime = entity.LastAccessTime,
			MD5Hash = entity.Data?.MD5Hash,
			Length = entity.Data?.Length,
			Content = entity.Data?.Content,
			ExtensionName = entity.Extension?.Name,
			MimeType = entity.Extension?.MimeType
		};

		return response;
	}
}
