using Application.Contracts.Responses.Attendance;

using AutoMapper;

using Domain.Extensions;
using Domain.Models.Attendance;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class ResponseProfiles
{
	/// <summary>
	/// The attendance response profile class.
	/// </summary>
	internal sealed class AttendanceResponseProfile : Profile
	{
		public AttendanceResponseProfile()
		{
			CreateMap<AttendanceModel, AttendanceResponse>()
				.ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => src.GetResultingWorkingHours()));
		}
	}
}
