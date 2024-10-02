﻿using System.Drawing;

using Application.Contracts.Requests.Attendance;
using Application.Contracts.Requests.Finance;
using Application.Contracts.Requests.Identity;
using Application.Contracts.Requests.Todo;

using BaseTests.Helpers;

using BB84.Extensions;

using Domain.Enumerators.Attendance;
using Domain.Enumerators.Finance;
using Domain.Enumerators.Todo;

using RegexPatterns = Domain.Common.Constants.RegexPatterns;

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
			ValidUntil = DateTime.MaxValue
		};

		return request;
	}

	internal static CardUpdateRequest GetCardUpdateRequest()
	{
		CardUpdateRequest request = new()
		{
			Type = CardType.DEBIT,
			ValidUntil = DateTime.MinValue
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
			Title = RandomHelper.GetString(50),
			Priority = PriorityLevelType.NONE,
			Reminder = DateTime.Today,
			Note = RandomHelper.GetString(1024)
		};

		return request;
	}

	internal static ItemUpdateRequest GetItemUpdateRequest()
	{
		ItemUpdateRequest request = new()
		{
			Title = RandomHelper.GetString(50),
			Priority = PriorityLevelType.NONE,
			Reminder = DateTime.Today,
			Note = RandomHelper.GetString(1024),
			Done = true
		};

		return request;
	}

	internal static TransactionCreateRequest GetTransactionCreateRequest()
	{
		TransactionCreateRequest request = new()
		{
			AccountNumber = RandomHelper.GetString(RegexPatterns.IBAN).RemoveWhitespace(),
			AmountEur = RandomHelper.GetDecimal(),
			BankCode = RandomHelper.GetString(25),
			BookingDate = RandomHelper.GetDateTime(),
			ClientBeneficiary = RandomHelper.GetString(250),
			CreditorId = RandomHelper.GetString(25),
			CustomerReference = RandomHelper.GetString(50),
			MandateReference = RandomHelper.GetString(50),
			PostingText = RandomHelper.GetString(100),
			Purpose = RandomHelper.GetString(4000),
			ValueDate = RandomHelper.GetDateTime()
		};

		return request;
	}

	internal static TransactionUpdateRequest GetTransactionUpdateRequest()
	{
		TransactionUpdateRequest request = new()
		{
			AccountNumber = RandomHelper.GetString(RegexPatterns.IBAN).RemoveWhitespace(),
			AmountEur = RandomHelper.GetDecimal(),
			BankCode = RandomHelper.GetString(25),
			BookingDate = RandomHelper.GetDateTime(),
			ClientBeneficiary = RandomHelper.GetString(250),
			CreditorId = RandomHelper.GetString(25),
			CustomerReference = RandomHelper.GetString(50),
			MandateReference = RandomHelper.GetString(50),
			PostingText = RandomHelper.GetString(100),
			Purpose = RandomHelper.GetString(4000),
			ValueDate = RandomHelper.GetDateTime()
		};

		return request;
	}
}
