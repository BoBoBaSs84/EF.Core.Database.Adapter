using Application.Contracts.Requests.Identity;

using AutoMapper;

using Domain.Entities.Identity;

namespace Application.Common.MappingProfiles.Requests;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class UserRequestProfile : Profile
{
	public UserRequestProfile()
	{
		CreateMap<UserCreateRequest, UserModel>();
		CreateMap<UserUpdateRequest, UserModel>();
	}
}
