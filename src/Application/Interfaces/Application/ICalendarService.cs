using Application.Contracts.Features.Requests;
using Application.Contracts.Features.Responses;
using Application.Contracts.Responses;
using Domain.Errors;

namespace Application.Interfaces.Application;

/// <summary>
/// The calendar service interface.
/// </summary>
public interface ICalendarService
{
	/// <summary>
	/// Should return all calendar entries as a paged list.
	/// </summary>
	/// <param name="parameters"></param>
	/// <param name="trackChanges"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>A paged list response.</returns>
	Task<ErrorOr<IPagedList<CalendarResponse>>> GetAllPaged(CalendarParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default);
}
