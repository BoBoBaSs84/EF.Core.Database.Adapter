using Domain.Entities.Identity;
using Domain.Enumerators;
using Domain.Extensions;
using Domain.Models.Attendance;
using Domain.Models.Common;
using Domain.Models.Finance;

using static BaseTests.Helpers.RandomHelper;
using static Domain.Constants.DomainConstants;
using static Domain.Constants.DomainConstants.Sql;

namespace BaseTests.Helpers;

public static class EntityHelper
{
	public static ICollection<CalendarModel> GetCalendarDays()
	{
		ICollection<CalendarModel> calendarDays = new List<CalendarModel>();
		DateTime startDate = new(DateTime.Now.Year, 1, 1), endDate = new(DateTime.Now.Year, 12, 31);
		while (!Equals(startDate, endDate))
		{
			CalendarModel calendarDay = new()
			{
				Date = startDate,
			};
			calendarDays.Add(calendarDay);
			startDate = startDate.AddDays(1);
		}
		return calendarDays;
	}

	public static UserModel GetNewUser(bool attendanceSeed = false, bool accountSeed = false)
	{
		string firstName = GetString(),
			lastName = GetString(),
			email = $"{firstName}.{lastName}@UnitTest.org",
			userName = $"{firstName}.{lastName}";

		UserModel userToReturn = new()
		{
			FirstName = firstName,
			LastName = lastName,
			DateOfBirth = GetDateTime(),
			Email = email,
			UserName = userName,
		};

		if (attendanceSeed)
			userToReturn.Attendances = GetNewAttendances(userToReturn);
		if (accountSeed)
			userToReturn.AccountUsers = GetNewAccountUsers(userToReturn);

		return userToReturn;
	}

	public static ICollection<AttendanceModel> GetNewAttendances(UserModel user, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<AttendanceModel> attendances = new();
		for (int i = 1; i <= entryAmount; i++)
			attendances.Add(new() { User = user, CalendarId = Guid.NewGuid(), DayType = (DayType)GetInt(3, 13) });
		return attendances;
	}

	public static ICollection<AccountUserModel> GetNewAccountUsers(UserModel user, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<AccountUserModel> accountUsers = new();
		for (int i = 1; i <= entryAmount; i++)
		{
			AccountModel newAccount = GetNewAccount();
			newAccount.AccountTransactions = GetNewAccountTransactions(newAccount);
			newAccount.Cards = GetNewCards(user, newAccount);
			accountUsers.Add(new() { User = user, Account = newAccount });
		}
		return accountUsers;
	}

	public static AccountModel GetNewAccount(string? iban = null, ICollection<AccountTransactionModel>? accountTransactions = null)
	{
		AccountModel accountToReturn = new()
		{
			IBAN = iban ?? GetString(RegexPatterns.IBAN),
			Provider = GetString(128),
			AccountTransactions = accountTransactions ?? default!
		};
		return accountToReturn;
	}

	public static ICollection<AccountTransactionModel> GetNewAccountTransactions(AccountModel account, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<AccountTransactionModel> accountTransactions = new();
		for (int i = 1; i <= entryAmount; i++)
			accountTransactions.Add(new() { Account = account, Transaction = GetNewTransaction() });
		return accountTransactions;
	}

	public static CardModel GetNewCard(UserModel user, AccountModel account, string? cardNumber = null, ICollection<CardTransactionModel>? cardTransactions = null)
	{
		CardModel cardToReturn = new()
		{
			Account = account,
			CardType = CardType.CREDIT,
			CardTransactions = cardTransactions ?? default!,
			PAN = cardNumber ?? GetString(RegexPatterns.CC),
			User = user
		};
		return cardToReturn;
	}

	public static ICollection<CardModel> GetNewCards(UserModel user, AccountModel account, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<CardModel> cardsToReturn = new();
		for (int i = 1; i <= entryAmount; i++)
		{
			CardModel newCard = GetNewCard(user, account);
			newCard.CardTransactions = GetNewCardTransactions(newCard);
			cardsToReturn.Add(newCard);
		}
		return cardsToReturn;
	}

	public static ICollection<CardTransactionModel> GetNewCardTransactions(CardModel card, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<CardTransactionModel> cardTransactions = new();
		for (int i = 1; i <= entryAmount; i++)
			cardTransactions.Add(new() { Card = card, Transaction = GetNewTransaction() });
		return cardTransactions;
	}

	public static TransactionModel GetNewTransaction()
	{
		TransactionModel transactionToReturn = new()
		{
			BookingDate = GetDateTime(),
			ValueDate = GetDateTime(),
			PostingText = GetString(MaxLength.MAX_100),
			ClientBeneficiary = GetString(MaxLength.MAX_250),
			Purpose = GetString(MaxLength.MAX_4000),
			AccountNumber = GetString(RegexPatterns.IBAN).RemoveWhitespace(),
			BankCode = GetString(MaxLength.MAX_25),
			AmountEur = GetInt(-100, 250),
			CreditorId = GetString(MaxLength.MAX_25),
			MandateReference = GetString(MaxLength.MAX_50),
			CustomerReference = GetString(MaxLength.MAX_50)
		};
		return transactionToReturn;
	}
}
