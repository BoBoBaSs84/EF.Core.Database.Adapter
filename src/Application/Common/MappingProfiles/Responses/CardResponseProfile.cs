using Application.Contracts.Responses.Finance;
using AutoMapper;
using Domain.Entities.Finance;

namespace Application.Common.MappingProfiles.Responses;

internal sealed class CardResponseProfile : Profile
{
	public CardResponseProfile()
	{
		CreateMap<Card, CardResponse>()
			.ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.CardType.Name));
	}
}
