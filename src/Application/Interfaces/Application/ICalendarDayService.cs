﻿using Application.Contracts.Responses;
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
	/// Should return the calendar day entities as a paged list, filtered by the parameters.
	/// </summary>
	/// <param name="parameters">The query parameters.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<CalendarDayResponse>>> GetPagedByParameters(CalendarDayParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should return the calendar day by its date.
	/// </summary>
	/// <param name="date">The date of the calendar day.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CalendarDayResponse>> GetByDate(DateTime date, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should return the calendar day by its identifier.
	/// </summary>
	/// <param name="id">The identifier of the calendar day.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CalendarDayResponse>> GetById(int id, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should return the current calendar day.
	/// </summary>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CalendarDayResponse>> GetCurrentDate(bool trackChanges = false, CancellationToken cancellationToken = default);
}
