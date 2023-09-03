using Application.Contracts.Requests.Finance;

using AutoMapper;

using Domain.Models.Finance;

namespace Application.Common.MappingProfiles.Requests;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class AccountRequestProfile : Profile
{
	public AccountRequestProfile()
	{
		CreateMap<AccountCreateRequest, Account>();
		CreateMap<AccountUpdateRequest, Account>();
	}
}
