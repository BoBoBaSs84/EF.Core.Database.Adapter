using Application.Contracts.Responses.Finance;

using AutoMapper;

using Domain.Models.Finance;

namespace Application.Common.MappingProfiles.Responses;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class CardResponseProfile : Profile
{
	public CardResponseProfile()
	{
		CreateMap<Card, CardResponse>();
	}
}
