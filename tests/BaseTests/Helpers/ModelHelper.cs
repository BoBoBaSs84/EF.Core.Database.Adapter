using System.Collections.Generic;

using Domain.Models.Identity;
using Domain.Enumerators;
using Domain.Extensions;
using Domain.Models.Attendance;
using Domain.Models.Common;
using Domain.Models.Finance;

using Tynamix.ObjectFiller;

using static BaseTests.Helpers.RandomHelper;
using static Domain.Constants.DomainConstants;
using static Domain.Constants.DomainConstants.Sql;

namespace BaseTests.Helpers;

public static class ModelHelper
{
	public static ICollection<AttendanceModel> GetNewAttendances(UserModel user, IList<CalendarModel> calendar, int entries = 10)
	{
		if (entries < 1)
			throw new ArgumentOutOfRangeException(nameof(entries));

		List<AttendanceModel> attendances = new();
		for (int i = 1; i <= entries; i++)
		{
			var calendarId = calendar[GetInt(0, calendar.Count)].Id;

			while (attendances.Exists(x=>x.CalendarId.Equals(calendarId)))
				calendarId = calendar[GetInt(0, calendar.Count)].Id;

			attendances.Add(new() { User = user, CalendarId = calendarId, AttendanceType = (AttendanceType)GetInt(4, 13) });
		}

		return attendances;
	}

	public static ICollection<AccountUserModel> GetNewAccountUsers(UserModel user, int accounts = 2, int accountTransactions = 10, int cards = 2, int cardTransactions = 10)
	{
		if (accounts < 1)
			throw new ArgumentOutOfRangeException(nameof(accounts));
		List<AccountUserModel> accountUsers = new();
		for (int i = 1; i <= accounts; i++)
		{
			AccountModel newAccount = GetNewAccount();
			newAccount.AccountTransactions = GetNewAccountTransactions(newAccount, accountTransactions);
			newAccount.Cards = GetNewCards(user, newAccount, cards, cardTransactions);
			accountUsers.Add(new() { User = user, Account = newAccount });
		}
		return accountUsers;
	}

	private static AccountModel GetNewAccount(string? iban = null, ICollection<AccountTransactionModel>? accountTransactions = null)
	{
		AccountModel accountToReturn = new()
		{
			IBAN = iban ?? GetString(RegexPatterns.IBAN),
			Provider = GetString(128),
			AccountTransactions = accountTransactions ?? default!
		};
		return accountToReturn;
	}

	private static ICollection<AccountTransactionModel> GetNewAccountTransactions(AccountModel account, int amount = 10)
	{
		if (amount < 1)
			throw new ArgumentOutOfRangeException(nameof(amount));
		List<AccountTransactionModel> accountTransactions = new();
		for (int i = 1; i <= amount; i++)
			accountTransactions.Add(new() { Account = account, Transaction = GetNewTransaction() });
		return accountTransactions;
	}

	private static CardModel GetNewCard(UserModel user, AccountModel account, string? cardNumber = null, ICollection<CardTransactionModel>? cardTransactions = null)
	{
		CardModel cardToReturn = new()
		{
			Account = account,
			CardType = (CardType)GetInt(1, 2),
			CardTransactions = cardTransactions ?? default!,
			PAN = cardNumber ?? GetString(RegexPatterns.CC),
			User = user
		};
		return cardToReturn;
	}

	private static ICollection<CardModel> GetNewCards(UserModel user, AccountModel account, int cardsAmount = 2, int transactionAmount = 10)
	{
		if (cardsAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(cardsAmount));
		List<CardModel> cardsToReturn = new();
		for (int i = 1; i <= cardsAmount; i++)
		{
			CardModel newCard = GetNewCard(user, account);
			newCard.CardTransactions = GetNewCardTransactions(newCard, transactionAmount);
			cardsToReturn.Add(newCard);
		}
		return cardsToReturn;
	}

	private static ICollection<CardTransactionModel> GetNewCardTransactions(CardModel card, int amount = 10)
	{
		if (amount < 1)
			throw new ArgumentOutOfRangeException(nameof(amount));

		List<CardTransactionModel> cardTransactions = new();
		for (int i = 1; i <= amount; i++)
			cardTransactions.Add(new() { Card = card, Transaction = GetNewTransaction() });
		return cardTransactions;
	}

	private static TransactionModel GetNewTransaction()
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
