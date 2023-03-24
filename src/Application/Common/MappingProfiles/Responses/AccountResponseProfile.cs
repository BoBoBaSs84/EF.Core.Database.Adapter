using Application.Contracts.Responses.Finance;
using AutoMapper;
using Domain.Entities.Finance;

namespace Application.Common.MappingProfiles.Responses;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class AccountResponseProfile : Profile
{
	public AccountResponseProfile()
	{
		CreateMap<Account, AccountResponse>();
	}
}
