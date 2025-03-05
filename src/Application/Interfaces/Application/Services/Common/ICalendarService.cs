using Application.Contracts.Responses.Common;
using Application.Features.Requests;
using Application.Features.Responses;

using BB84.Home.Domain.Errors;

namespace Application.Interfaces.Application.Services.Common;

/// <summary>
/// The interface for the calendar service.
/// </summary>
public interface ICalendarService
{
	/// <summary>
	/// Returns the calendar information based on the provided <paramref name="date"/>.
	/// </summary>
	/// <param name="date">The date to use.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	ErrorOr<CalendarResponse> GetByDate(DateTime date);

	/// <summary>
	/// Returns the calendar information based on the provided <paramref name="parameters"/>.
	/// </summary>
	/// <param name="parameters">The calendar search parameters to use.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	ErrorOr<IPagedList<CalendarResponse>> GetPagedByParameters(CalendarParameters parameters);

	/// <summary>
	/// Returns the calendar information based current date.
	/// </summary>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	ErrorOr<CalendarResponse> GetCurrent();
}
