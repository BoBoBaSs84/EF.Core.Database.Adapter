using Domain.Entities.Finance;
using Domain.Entities.Identity;
using Domain.Entities.Private;
using Domain.Extensions;
using static BaseTests.Helpers.RandomHelper;
using static Domain.Constants.DomainConstants;
using static Domain.Constants.DomainConstants.Sql;

namespace ApplicationTests.Helpers;

public static class EntityHelper
{
	public static ICollection<CalendarDay> GetCalendarDays()
	{
		ICollection<CalendarDay> calendarDays = new List<CalendarDay>();
		DateTime startDate = new(DateTime.Now.Year, 1, 1), endDate = new(DateTime.Now.Year, 12, 31);
		while (!Equals(startDate, endDate))
		{
			CalendarDay calendarDay = new()
			{
				Date = startDate,
				DayTypeId = startDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday ? 2 : 1
			};
			calendarDays.Add(calendarDay);
			startDate = startDate.AddDays(1);
		}
		return calendarDays;
	}

	public static User GetNewUser(bool attendanceSeed = false, bool accountSeed = false)
	{
		string firstName = GetString(),
			lastName = GetString(),
			email = $"{firstName}.{lastName}@UnitTest.org",
			userName = $"{firstName}.{lastName}";

		User userToReturn = new()
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

	public static ICollection<Attendance> GetNewAttendances(User user, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<Attendance> attendances = new();
		for (int i = 1; i <= entryAmount; i++)
			attendances.Add(new() { User = user, CalendarDayId = i, DayTypeId = GetInt(3, 13) });
		return attendances;
	}

	public static ICollection<AccountUser> GetNewAccountUsers(User user, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<AccountUser> accountUsers = new();
		for (int i = 1; i <= entryAmount; i++)
		{
			Account newAccount = GetNewAccount();
			newAccount.AccountTransactions = GetNewAccountTransactions(newAccount);
			newAccount.Cards = GetNewCards(user, newAccount);
			accountUsers.Add(new() { User = user, Account = newAccount });
		}
		return accountUsers;
	}

	public static Account GetNewAccount(string? iban = null, ICollection<AccountTransaction>? accountTransactions = null)
	{
		Account accountToReturn = new()
		{
			IBAN = iban ?? GetString(RegexPatterns.IBAN),
			Provider = GetString(128),
			AccountTransactions = accountTransactions ?? default!
		};
		return accountToReturn;
	}

	public static ICollection<AccountTransaction> GetNewAccountTransactions(Account account, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<AccountTransaction> accountTransactions = new();
		for (int i = 1; i <= entryAmount; i++)
			accountTransactions.Add(new() { Account = account, Transaction = GetNewTransaction() });
		return accountTransactions;
	}

	public static Card GetNewCard(User user, Account account, string? cardNumber = null, ICollection<CardTransaction>? cardTransactions = null)
	{
		Card cardToReturn = new()
		{
			Account = account,
			CardTypeId = 1,
			CardTransactions = cardTransactions ?? default!,
			PAN = cardNumber ?? GetString(RegexPatterns.CC),
			User = user
		};
		return cardToReturn;
	}

	public static ICollection<Card> GetNewCards(User user, Account account, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<Card> cardsToReturn = new();
		for (int i = 1; i <= entryAmount; i++)
		{
			Card newCard = GetNewCard(user, account);
			newCard.CardTransactions = GetNewCardTransactions(newCard);
			cardsToReturn.Add(newCard);
		}
		return cardsToReturn;
	}

	public static ICollection<CardTransaction> GetNewCardTransactions(Card card, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<CardTransaction> cardTransactions = new();
		for (int i = 1; i <= entryAmount; i++)
			cardTransactions.Add(new() { Card = card, Transaction = GetNewTransaction() });
		return cardTransactions;
	}

	public static Transaction GetNewTransaction()
	{
		Transaction transactionToReturn = new()
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