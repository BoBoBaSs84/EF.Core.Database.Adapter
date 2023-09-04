using Application.Contracts.Responses.Enumerators;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Logging;

using AutoMapper;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Extensions;

using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// The enumerator service class.
/// </summary>
internal sealed class EnumeratorService : IEnumeratorService
{
	private readonly ILoggerService<EnumeratorService> _loggerService;
	private readonly IMapper _mapper;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	/// <summary>
	/// Initilizes an instance of the enumerator service class.
	/// </summary>
	/// <param name="loggerService">The logger service instance to use.</param>
	/// <param name="mapper">The auto mapper instance to use.</param>
	public EnumeratorService(ILoggerService<EnumeratorService> loggerService, IMapper mapper)
	{
		_loggerService = loggerService;
		_mapper = mapper;
	}

	public ErrorOr<IEnumerable<CardTypeResponse>> GetCardTypes()
	{
		try
		{
			IEnumerable<CardType> cardTypes = CardType.CREDIT.ToList();

			return _mapper.Map<IEnumerable<CardTypeResponse>>(cardTypes).ToList();
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return EnumeratorServiceErrors.GetCardTypesFailed;
		}
	}

	public ErrorOr<IEnumerable<DayTypeResponse>> GetDayTypes()
	{
		try
		{
			IEnumerable<DayType> cardTypes = DayType.HOLIDAY.ToList();

			return _mapper.Map<IEnumerable<DayTypeResponse>>(cardTypes).ToList();
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			return EnumeratorServiceErrors.GetDayTypesFailed;
		}
	}
}
