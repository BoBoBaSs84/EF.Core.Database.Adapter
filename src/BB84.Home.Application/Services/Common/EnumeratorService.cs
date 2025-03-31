using AutoMapper;

using BB84.Extensions;
using BB84.Home.Application.Contracts.Responses.Common;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Application.Services.Common;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Domain.Enumerators;
using BB84.Home.Domain.Enumerators.Attendance;
using BB84.Home.Domain.Enumerators.Documents;
using BB84.Home.Domain.Enumerators.Finance;
using BB84.Home.Domain.Enumerators.Todo;
using BB84.Home.Domain.Errors;

using Microsoft.Extensions.Logging;

namespace BB84.Home.Application.Services.Common;

/// <summary>
/// The enumerator service class.
/// </summary>
/// <param name="loggerService">The logger service instance to use.</param>
/// <param name="mapper">The auto mapper instance to use.</param>
internal sealed class EnumeratorService(ILoggerService<EnumeratorService> loggerService, IMapper mapper) : IEnumeratorService
{
	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	public ErrorOr<IEnumerable<AccountTypeResponse>> GetAccountTypes()
	{
		try
		{
			IEnumerable<AccountType> accountTypes = AccountType.CHECKING.GetValues();

			return mapper.Map<IEnumerable<AccountTypeResponse>>(accountTypes).ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return EnumeratorServiceErrors.GetAccountTypesFailed;
		}
	}

	public ErrorOr<IEnumerable<AttendanceTypeResponse>> GetAttendanceTypes()
	{
		try
		{
			IEnumerable<AttendanceType> attendanceTypes = AttendanceType.HOLIDAY.GetValues();

			return mapper.Map<IEnumerable<AttendanceTypeResponse>>(attendanceTypes).ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return EnumeratorServiceErrors.GetAttendanceTypesFailed;
		}
	}

	public ErrorOr<IEnumerable<CardTypeResponse>> GetCardTypes()
	{
		try
		{
			IEnumerable<CardType> cardTypes = CardType.CREDIT.GetValues();

			return mapper.Map<IEnumerable<CardTypeResponse>>(cardTypes).ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return EnumeratorServiceErrors.GetCardTypesFailed;
		}
	}

	public ErrorOr<IEnumerable<DocumentTypeResponse>> GetDocumentTypes()
	{
		try
		{
			IEnumerable<DocumentTypes> documentTypes = DocumentTypes.None.GetValues();

			return mapper.Map<IEnumerable<DocumentTypeResponse>>(documentTypes).ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return EnumeratorServiceErrors.GetDocumentTypesFailed;
		}
	}

	public ErrorOr<IEnumerable<PriorityLevelTypeResponse>> GetPriorityLevelTypes()
	{
		try
		{
			IEnumerable<PriorityLevelType> priorityLevelTypes = PriorityLevelType.NONE.GetValues();

			return mapper.Map<IEnumerable<PriorityLevelTypeResponse>>(priorityLevelTypes).ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return EnumeratorServiceErrors.GetPriorityLevelTypesFailed;
		}
	}

	public ErrorOr<IEnumerable<RoleTypeResponse>> GetRoleTypes()
	{
		try
		{
			IEnumerable<RoleType> roleTypes = RoleType.ADMINISTRATOR.GetValues();

			return mapper.Map<IEnumerable<RoleTypeResponse>>(roleTypes).ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return EnumeratorServiceErrors.GetRoleTypesFailed;
		}
	}

	public ErrorOr<IEnumerable<WorkDayTypeResponse>> GetWorkDayTypes()
	{
		try
		{
			IEnumerable<WorkDayTypes> workDayTypes = WorkDayTypes.Sunday.GetValues();

			return mapper.Map<IEnumerable<WorkDayTypeResponse>>(workDayTypes).ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return EnumeratorServiceErrors.GetWorkDayTypesFailed;
		}
	}
}
