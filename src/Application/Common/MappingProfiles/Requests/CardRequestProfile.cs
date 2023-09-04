using Application.Contracts.Requests.Finance;

using AutoMapper;

using Domain.Models.Finance;

namespace Application.Common.MappingProfiles.Requests;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class CardRequestProfile : Profile
{
	public CardRequestProfile()
	{
		CreateMap<CardCreateRequest, CardModel>();
		CreateMap<CardUpdateRequest, CardModel>();
	}
}
