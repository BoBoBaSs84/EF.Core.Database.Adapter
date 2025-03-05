using Application.Contracts.Requests.Documents;

using AutoMapper;

using BB84.Home.Domain.Entities.Documents;

namespace Application.Profiles.Requests;

/// <summary>
/// The documents request profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class DocumentsRequestProfile : Profile
{
	public DocumentsRequestProfile()
	{
		CreateMap<DocumentCreateRequest, DocumentEntity>();
		CreateMap<DocumentUpdateRequest, DocumentEntity>();
	}
}
