using BB84.Extensions;
using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Entities.Identity;
using BB84.Home.Domain.Enumerators.Attendance;
using BB84.Home.Domain.Enumerators.Finance;

namespace BB84.Home.Infrastructure.Tests.Helpers;

public static class ModelHelper
{
	public static ICollection<AttendanceEntity> GetNewAttendances(UserEntity user, IList<DateTime> calendar, int entries = 10)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(entries, 1);

		List<AttendanceEntity> attendances = [];
		for (int i = 1; i <= entries; i++)
		{
			DateTime calendarDate = calendar[GetInt(0, calendar.Count)];

			while (attendances.Exists(x => x.Date.Equals(calendarDate)))
				calendarDate = calendar[GetInt(0, calendar.Count)];

			attendances.Add(new() { User = user, Date = calendarDate, Type = (AttendanceType)GetInt(4, 13) });
		}

		return attendances;
	}

	public static ICollection<AccountUserEntity> GetNewAccountUsers(UserEntity user, int accounts = 2, int accountTransactions = 10, int cards = 2, int cardTransactions = 10)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(accounts, 1);

		List<AccountUserEntity> accountUsers = [];
		for (int i = 1; i <= accounts; i++)
		{
			AccountEntity newAccount = GetNewAccount();
			newAccount.Transactions = GetNewAccountTransactions(newAccount, accountTransactions);
			newAccount.Cards = GetNewCards(user, newAccount, cards, cardTransactions);
			accountUsers.Add(new() { User = user, Account = newAccount });
		}
		return accountUsers;
	}

	private static AccountEntity GetNewAccount(string? iban = null, ICollection<AccountTransactionEntity>? accountTransactions = null)
	{
		AccountEntity accountToReturn = new()
		{
			IBAN = iban ?? GetString(20),
			Provider = GetString(128),
			Transactions = accountTransactions ?? default!
		};
		return accountToReturn;
	}

	private static ICollection<AccountTransactionEntity> GetNewAccountTransactions(AccountEntity account, int amount = 10)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(amount, 1);

		List<AccountTransactionEntity> accountTransactions = [];
		for (int i = 1; i <= amount; i++)
			accountTransactions.Add(new() { Account = account, Transaction = GetNewTransaction() });
		return accountTransactions;
	}

	private static CardEntity GetNewCard(UserEntity user, AccountEntity account, string? cardNumber = null, ICollection<CardTransactionEntity>? cardTransactions = null)
	{
		CardEntity cardToReturn = new()
		{
			Account = account,
			Type = (CardType)GetInt(1, 2),
			Transactions = cardTransactions ?? default!,
			PAN = cardNumber ?? GetString(20),
			User = user
		};
		return cardToReturn;
	}

	private static ICollection<CardEntity> GetNewCards(UserEntity user, AccountEntity account, int cardsAmount = 2, int transactionAmount = 10)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(cardsAmount, 1);

		List<CardEntity> cardsToReturn = [];
		for (int i = 1; i <= cardsAmount; i++)
		{
			CardEntity newCard = GetNewCard(user, account);
			newCard.Transactions = GetNewCardTransactions(newCard, transactionAmount);
			cardsToReturn.Add(newCard);
		}
		return cardsToReturn;
	}

	private static ICollection<CardTransactionEntity> GetNewCardTransactions(CardEntity card, int amount = 10)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(amount, 1);

		List<CardTransactionEntity> cardTransactions = [];
		for (int i = 1; i <= amount; i++)
			cardTransactions.Add(new() { Card = card, Transaction = GetNewTransaction() });
		return cardTransactions;
	}

	private static TransactionEntity GetNewTransaction()
	{
		TransactionEntity transactionToReturn = new()
		{
			BookingDate = GetDateTime(),
			ValueDate = GetDateTime(),
			PostingText = GetString(50),
			ClientBeneficiary = GetString(150),
			Purpose = GetString(1000),
			AccountNumber = GetString(20).RemoveWhitespace(),
			BankCode = GetString(25),
			AmountEur = GetInt(-100, 250),
			CreditorId = GetString(25),
			MandateReference = GetString(50),
			CustomerReference = GetString(50)
		};
		return transactionToReturn;
	}
}
