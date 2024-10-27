using System.Drawing;
using Domain.Enumerators.Documents;
using Application.Contracts.Requests.Attendance;
using Application.Contracts.Requests.Documents;
using Application.Contracts.Requests.Finance;
using Application.Contracts.Requests.Identity;
using Application.Contracts.Requests.Todo;

using BaseTests.Helpers;

using BB84.Extensions;

using Domain.Enumerators.Attendance;
using Domain.Enumerators.Finance;
using Domain.Enumerators.Todo;

using static Application.Common.ApplicationConstants;

using DateStatics = Application.Common.ApplicationStatics.DateStatics;

namespace ApplicationTests.Helpers;

internal static class RequestHelper
{
	internal static AccountCreateRequest GetAccountCreateRequest()
	{
		AccountCreateRequest request = new()
		{
			IBAN = RandomHelper.GetString(RegexPatterns.IBAN).RemoveWhitespace(),
			Provider = RandomHelper.GetString(25),
			Type = AccountType.SAVINGS,
			Cards = [GetCardCreateRequest()]
		};

		return request;
	}

	internal static AccountUpdateRequest GetAccountUpdateRequest()
	{
		AccountUpdateRequest request = new()
		{
			Provider = RandomHelper.GetString(25),
			Type = AccountType.CHECKING
		};

		return request;
	}

	internal static CardCreateRequest GetCardCreateRequest()
	{
		CardCreateRequest request = new()
		{
			PAN = RandomHelper.GetString(RegexPatterns.PAN).RemoveWhitespace(),
			Type = CardType.CREDIT,
			ValidUntil = RandomHelper.GetDateTime(DateStatics.MinDate, DateStatics.MaxDate)
		};

		return request;
	}

	internal static CardUpdateRequest GetCardUpdateRequest()
	{
		CardUpdateRequest request = new()
		{
			Type = CardType.DEBIT,
			ValidUntil = RandomHelper.GetDateTime(DateStatics.MinDate, DateStatics.MaxDate)
		};

		return request;
	}

	internal static AttendanceCreateRequest GetAttendanceCreateRequest()
	{
		AttendanceCreateRequest request = new()
		{
			Date = DateTime.Today,
			Type = AttendanceType.WORKDAY,
			StartTime = new(6, 0, 0),
			EndTime = new(16, 0, 0),
			BreakTime = new(0, 45, 0)
		};

		return request;
	}

	internal static AttendanceUpdateRequest GetAttendanceUpdateRequest()
	{
		AttendanceUpdateRequest request = new()
		{
			Id = Guid.NewGuid(),
			Type = AttendanceType.WORKDAY,
			StartTime = new(6, 0, 0),
			EndTime = new(16, 0, 0),
			BreakTime = new(0, 45, 0)
		};

		return request;
	}

	internal static DocumentCreateRequest GetDocumentCreateRequest()
	{
		DocumentCreateRequest request = new()
		{
			Name = RandomHelper.GetString(18),
			Extension = RandomHelper.GetString(3),
			Directory = @"C:\",
			Flags = DocumentTypes.None,
			CreationTime = DateTime.Today.AddDays(-2),
			LastAccessTime = DateTime.Today,
			LastWriteTime = DateTime.Today.AddDays(-1),
			Data = [01, 12, 23, 34, 45, 56, 67, 78, 89, 90]
		};

		return request;
	}

	internal static UserCreateRequest GetUserCreateRequest()
	{
		UserCreateRequest request = new()
		{
			FirstName = RandomHelper.GetString(50),
			LastName = RandomHelper.GetString(50),
			Email = RandomHelper.GetString(RegexPatterns.Email),
			UserName = RandomHelper.GetString(50),
			Password = RandomHelper.GetString(50),
		};
		return request;
	}

	internal static UserUpdateRequest GetUserUpdateRequest()
	{
		UserUpdateRequest request = new()
		{
			FirstName = RandomHelper.GetString(50),
			MiddleName = RandomHelper.GetString(50),
			LastName = RandomHelper.GetString(50),
			DateOfBirth = DateTime.Today,
			Email = RandomHelper.GetString(RegexPatterns.Email),
			PhoneNumber = "+1234567890",
			Picture = null,
			Preferences = null,
		};
		return request;
	}

	internal static ListCreateRequest GetListCreateRequest()
	{
		ListCreateRequest request = new()
		{
			Title = RandomHelper.GetString(25),
			Color = Color.White.ToRGBHexString()
		};

		return request;
	}

	internal static ListUpdateRequest GetListUpdateRequest()
	{
		ListUpdateRequest request = new()
		{
			Title = RandomHelper.GetString(25),
			Color = Color.Black.ToRGBHexString()
		};

		return request;
	}

	internal static ItemCreateRequest GetItemCreateRequest()
	{
		ItemCreateRequest request = new()
		{
			Title = RandomHelper.GetString(128),
			Priority = PriorityLevelType.NONE,
			Reminder = RandomHelper.GetDateTime(DateStatics.MinDate, DateStatics.MaxDate),
			Note = RandomHelper.GetString(1024)
		};

		return request;
	}

	internal static ItemUpdateRequest GetItemUpdateRequest()
	{
		ItemUpdateRequest request = new()
		{
			Title = RandomHelper.GetString(128),
			Priority = PriorityLevelType.NONE,
			Reminder = RandomHelper.GetDateTime(DateStatics.MinDate, DateStatics.MaxDate),
			Note = RandomHelper.GetString(1024),
			Done = true
		};

		return request;
	}

	internal static TransactionCreateRequest GetTransactionCreateRequest()
	{
		DateTime bookingDate = RandomHelper.GetDateTime(DateStatics.MinDate, DateStatics.MaxDate);
		DateTime valueDate = bookingDate.AddDays(1);

		TransactionCreateRequest request = new()
		{
			AccountNumber = RandomHelper.GetString(RegexPatterns.IBAN).RemoveWhitespace(),
			AmountEur = (decimal)RandomHelper.GetFloat() * RandomHelper.GetInt(10000),
			BankCode = RandomHelper.GetString(RegexPatterns.BIC).RemoveWhitespace(),
			BookingDate = bookingDate,
			ClientBeneficiary = RandomHelper.GetString(250),
			CreditorId = RandomHelper.GetString(25),
			CustomerReference = RandomHelper.GetString(50),
			MandateReference = RandomHelper.GetString(50),
			PostingText = RandomHelper.GetString(100),
			Purpose = RandomHelper.GetString(4000),
			ValueDate = valueDate
		};

		return request;
	}

	internal static TransactionUpdateRequest GetTransactionUpdateRequest()
	{
		DateTime bookingDate = RandomHelper.GetDateTime(DateStatics.MinDate, DateStatics.MaxDate);
		DateTime valueDate = bookingDate.AddDays(1);

		TransactionUpdateRequest request = new()
		{
			AccountNumber = RandomHelper.GetString(RegexPatterns.IBAN).RemoveWhitespace(),
			AmountEur = (decimal)RandomHelper.GetFloat() * RandomHelper.GetInt(10000),
			BankCode = RandomHelper.GetString(RegexPatterns.BIC).RemoveWhitespace(),
			BookingDate = bookingDate,
			ClientBeneficiary = RandomHelper.GetString(250),
			CreditorId = RandomHelper.GetString(25),
			CustomerReference = RandomHelper.GetString(50),
			MandateReference = RandomHelper.GetString(50),
			PostingText = RandomHelper.GetString(100),
			Purpose = RandomHelper.GetString(4000),
			ValueDate = valueDate
		};

		return request;
	}
}
