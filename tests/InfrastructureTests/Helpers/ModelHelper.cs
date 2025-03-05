using BB84.Extensions;
using BB84.Home.BaseTests.Helpers;
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
			DateTime calendarDate = calendar[RandomHelper.GetInt(0, calendar.Count)];

			while (attendances.Exists(x => x.Date.Equals(calendarDate)))
				calendarDate = calendar[RandomHelper.GetInt(0, calendar.Count)];

			attendances.Add(new() { User = user, Date = calendarDate, Type = (AttendanceType)RandomHelper.GetInt(4, 13) });
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
			IBAN = iban ?? RandomHelper.GetString(20),
			Provider = RandomHelper.GetString(128),
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
			Type = (CardType)RandomHelper.GetInt(1, 2),
			Transactions = cardTransactions ?? default!,
			PAN = cardNumber ?? RandomHelper.GetString(20),
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
			BookingDate = RandomHelper.GetDateTime(),
			ValueDate = RandomHelper.GetDateTime(),
			PostingText = RandomHelper.GetString(50),
			ClientBeneficiary = RandomHelper.GetString(150),
			Purpose = RandomHelper.GetString(1000),
			AccountNumber = RandomHelper.GetString(20).RemoveWhitespace(),
			BankCode = RandomHelper.GetString(25),
			AmountEur = RandomHelper.GetInt(-100, 250),
			CreditorId = RandomHelper.GetString(25),
			MandateReference = RandomHelper.GetString(50),
			CustomerReference = RandomHelper.GetString(50)
		};
		return transactionToReturn;
	}
}
