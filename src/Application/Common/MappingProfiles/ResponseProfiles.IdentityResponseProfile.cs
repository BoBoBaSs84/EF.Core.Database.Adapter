using Application.Contracts.Responses.Identity;

using AutoMapper;

using Domain.Models.Identity;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class ResponseProfiles
{
	/// <summary>
	/// The identity response profile class.
	/// </summary>
	internal sealed class IdentityResponseProfile : Profile
	{
		public IdentityResponseProfile()
		{
			CreateMap<UserModel, UserResponse>();

			CreateMap<PreferencesModel, PreferencesResponse>();
			CreateMap<AttendancePreferencesModel, AttendancePreferencesResponse>();
		}
	}
}
