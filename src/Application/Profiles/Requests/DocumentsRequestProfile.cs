﻿using AutoMapper;

using BB84.Home.Application.Contracts.Requests.Documents;
using BB84.Home.Domain.Entities.Documents;

namespace BB84.Home.Application.Profiles.Requests;

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
