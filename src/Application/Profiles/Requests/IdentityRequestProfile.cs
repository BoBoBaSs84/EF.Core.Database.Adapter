using Application.Contracts.Requests.Identity;

using AutoMapper;

using Domain.Entities.Identity;
using Domain.Models.Identity;

namespace Application.Profiles.Requests;

/// <summary>
/// The identity request profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class IdentityRequestProfile : Profile
{
	public IdentityRequestProfile()
	{
		CreateMap<UserCreateRequest, UserModel>();
		CreateMap<UserUpdateRequest, UserModel>();

		CreateMap<PreferencesRequest, PreferencesModel>();
		CreateMap<AttendancePreferencesRequest, AttendancePreferencesModel>();
	}
}
