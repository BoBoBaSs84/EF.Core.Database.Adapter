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
internal sealed class EnumeratorService(ILoggerService<EnumeratorService> loggerService) : IEnumeratorService
{
	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	public ErrorOr<IEnumerable<AccountTypeResponse>> GetAccountTypes()
	{
		try
		{
			IEnumerable<AccountType> accountTypes = AccountType.Checking.GetValues();

			return accountTypes.Select(x => new AccountTypeResponse(x)).ToList();
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
			IEnumerable<AttendanceType> attendanceTypes = AttendanceType.Holiday.GetValues();

			return attendanceTypes.Select(x => new AttendanceTypeResponse(x)).ToList();
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
			IEnumerable<CardType> cardTypes = CardType.Credit.GetValues();

			return cardTypes.Select(x => new CardTypeResponse(x)).ToList();
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

			return documentTypes.Select(x => new DocumentTypeResponse(x)).ToList();
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
			IEnumerable<PriorityLevelType> priorityLevelTypes = PriorityLevelType.None.GetValues();

			return priorityLevelTypes.Select(x => new PriorityLevelTypeResponse(x)).ToList();
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
			IEnumerable<RoleType> roleTypes = RoleType.Administrator.GetValues();

			return roleTypes.Select(x => new RoleTypeResponse(x)).ToList();
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

			return workDayTypes.Select(x => new WorkDayTypeResponse(x)).ToList();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return EnumeratorServiceErrors.GetWorkDayTypesFailed;
		}
	}
}
