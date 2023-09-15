using Application.Contracts.Requests.Attendance;

using AutoMapper;

using Domain.Models.Attendance;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class RequestProfiles
{
	/// <summary>
	/// The attendance request profile class.
	/// </summary>
	internal sealed class AttendanceRequestProfile : Profile
	{
		public AttendanceRequestProfile()
		{
			CreateMap<AttendanceCreateRequest, AttendanceModel>();
			CreateMap<AttendanceUpdateRequest, AttendanceModel>();
		}
	}
}
