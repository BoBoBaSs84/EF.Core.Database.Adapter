using Application.Contracts.Responses.Identity;

using AutoMapper;

using Domain.Entities.Identity;
using Domain.Models.Identity;

namespace Application.Profiles.Responses;

/// <summary>
/// The identity response profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class IdentityResponseProfile : Profile
{
	public IdentityResponseProfile()
	{
		CreateMap<UserModel, UserResponse>();

		CreateMap<PreferencesModel, PreferencesResponse>();
		CreateMap<AttendancePreferencesModel, AttendancePreferencesResponse>();
	}
}
