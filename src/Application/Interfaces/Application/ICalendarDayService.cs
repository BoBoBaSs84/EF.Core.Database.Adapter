using Application.Contracts.Responses.Common;
using Application.Features.Requests;
using Application.Features.Responses;

using Domain.Errors;

namespace Application.Interfaces.Application;

/// <summary>
/// The calendar service interface.
/// </summary>
[SuppressMessage("Naming", "CA1716", Justification = "Usable in all available languages in .NET")]
public interface ICalendarDayService
{
	/// <summary>
	/// Returns the calendar day entities as a paged list, filtered by the <paramref name="parameters"/>.
	/// </summary>
	/// <param name="parameters">The query parameters.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<CalendarDayResponse>>> Get(CalendarDayParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the calendar day by its <paramref name="date"/>.
	/// </summary>
	/// <param name="date">The date of the calendar day.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CalendarDayResponse>> Get(DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the calendar day by its <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The identifier of the calendar day.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CalendarDayResponse>> Get(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the current calendar day.
	/// </summary>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CalendarDayResponse>> Get(bool trackChanges = false, CancellationToken cancellationToken = default);
}
