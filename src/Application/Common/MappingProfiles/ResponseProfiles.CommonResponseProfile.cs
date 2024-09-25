using Application.Contracts.Responses.Common;

using AutoMapper;

using BB84.Extensions;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class ResponseProfiles
{
	/// <inheritdoc/>
	internal sealed class CommonResponseProfile : Profile
	{
		public CommonResponseProfile()
		{
			CreateMap<DateTime, CalendarResponse>()
				.ForMember(t => t.Year, o => o.MapFrom(s => s.Date.Year))
				.ForMember(t => t.Month, o => o.MapFrom(s => s.Date.Month))
				.ForMember(t => t.Week, o => o.MapFrom(s => s.Date.WeekOfYear()))
				.ForMember(t => t.DayOfWeek, o => o.MapFrom(s => s.Date.DayOfWeek))
				.ForMember(t => t.DayOfYear, o => o.MapFrom(s => s.Date.DayOfYear))
				.ForMember(t => t.StartOfWeek, o => o.MapFrom(s => s.Date.StartOfWeek(DayOfWeek.Monday)))
				.ForMember(t => t.EndOfWeek, o => o.MapFrom(s => s.Date.EndOfWeek(DayOfWeek.Monday)))
				.ForMember(t => t.StartOfMonth, o => o.MapFrom(s => s.Date.StartOfMonth()))
				.ForMember(t => t.EndOfMonth, o => o.MapFrom(s => s.Date.EndOfMonth()));
		}
	}
}
