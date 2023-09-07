using Application.Contracts.Responses.Common;
using Application.Features.Requests;
using Application.Features.Responses;

using Domain.Errors;

namespace Application.Interfaces.Application;

/// <summary>
/// The calendar service interface.
/// </summary>
[SuppressMessage("Naming", "CA1716", Justification = "Usable in all available languages in .NET")]
public interface ICalendarService
{
	/// <summary>
	/// Returns the calendar entries as a paged list, filtered by the <paramref name="parameters"/>.
	/// </summary>
	/// <param name="parameters">The query parameters.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<CalendarResponse>>> Get(CalendarParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the calendar entry by its <paramref name="date"/>.
	/// </summary>
	/// <param name="date">The date of the calendar entry.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CalendarResponse>> Get(DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the calendar entry by its identifier.
	/// </summary>
	/// <param name="id">The identifier of the calendar entry.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CalendarResponse>> Get(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the current calendar entry.
	/// </summary>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CalendarResponse>> Get(bool trackChanges = false, CancellationToken cancellationToken = default);
}
