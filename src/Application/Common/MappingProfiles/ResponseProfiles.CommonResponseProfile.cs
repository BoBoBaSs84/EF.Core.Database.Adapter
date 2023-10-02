using Application.Contracts.Responses.Common;

using AutoMapper;

using Domain.Extensions;
using Domain.Models.Common;

namespace Application.Common.MappingProfiles;	

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class ResponseProfiles
{
	/// <summary>
	/// The common response profile class.
	/// </summary>
	internal sealed class CommonResponseProfile : Profile
	{
		public CommonResponseProfile()
		{
			CreateMap<CalendarModel, CalendarResponse>()
				.ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Date.Year))
				.ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Date.Month))
				.ForMember(dest => dest.IsoWeek, opt => opt.MapFrom(src => src.Date.WeekOfYear()))
				.ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.Date.DayOfWeek))
				.ForMember(dest => dest.DayOfYear, opt => opt.MapFrom(src => src.Date.DayOfYear))
				.ForMember(dest => dest.EndOfMonth, opt => opt.MapFrom(src => src.Date.EndOfMonth()));
		}
	}
}
