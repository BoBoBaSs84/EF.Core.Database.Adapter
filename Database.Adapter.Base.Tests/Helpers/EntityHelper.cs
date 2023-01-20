using Database.Adapter.Entities.Extensions;
using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Entities.Contexts.Timekeeping;
using static Database.Adapter.Entities.Constants;
using static Database.Adapter.Entities.Statics;

namespace Database.Adapter.Base.Tests.Helpers;

public static class EntityHelper
{
	public static User GetNewUser(bool attendanceSeed = false, bool accountSeed = false)
	{
		string firstName = RandomHelper.GetString(12),
			lastName = RandomHelper.GetString(12),
			email = $"{firstName}.{lastName}@UnitTest.org";

		User userToReturn = new()
		{
			FirstName = firstName,
			LastName = lastName,
			Email = email,
			NormalizedEmail = email.ToUpper(CurrentCulture),
			UserName = Constants.UnitTestUserName,
			NormalizedUserName = Constants.UnitTestUserName.ToUpper(CurrentCulture),
		};

		if (attendanceSeed)
			userToReturn.Attendances = GetNewAttendanceCollection(userToReturn);
		if (accountSeed)
			userToReturn.AccountUsers = GetNewAccountUserColection(userToReturn);

		return userToReturn;
	}

	public static ICollection<Attendance> GetNewAttendanceCollection(User user, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<Attendance> attendances = new();
		for (int i = 1; i <= entryAmount; i++)
			attendances.Add(new() { User = user, CalendarDayId = i, DayTypeId = RandomHelper.GetInt(3, 13) });
		return attendances;
	}

	public static ICollection<AccountUser> GetNewAccountUserColection(User user, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<AccountUser> accountUsers = new();
		for (int i = 1; i <= entryAmount; i++)
		{
			Account newAccount = GetNewAccount();
			newAccount.AccountTransactions = GetNewAccountTransactionColection(newAccount);
			accountUsers.Add(new() { User = user, Account = newAccount });
		}
		return accountUsers;
	}

	public static Account GetNewAccount(string? iban = null, ICollection<AccountTransaction>? accountTransactions = null)
	{
		Account accountToReturn = new()
		{
			IBAN = iban ?? RandomHelper.GetString(Regex.IBAN),
			Provider = RandomHelper.GetString(128),
			AccountTransactions = accountTransactions ?? default!
		};
		return accountToReturn;
	}

	public static ICollection<AccountTransaction> GetNewAccountTransactionColection(Account account, int entryAmount = 2)
	{
		if (entryAmount < 1)
			throw new ArgumentOutOfRangeException(nameof(entryAmount));
		List<AccountTransaction> accountTransactions = new();
		for (int i = 1; i <= entryAmount; i++)
			accountTransactions.Add(new() { Account = account, Transaction = GetNewTransaction() });
		return accountTransactions;
	}

	public static Transaction GetNewTransaction()
	{
		Transaction transactionToReturn = new()
		{
			BookingDate = RandomHelper.GetDateTime(),
			ValueDate = RandomHelper.GetDateTime(),
			PostingText = RandomHelper.GetString(128),
			ClientBeneficiary = RandomHelper.GetString(128),
			Purpose = RandomHelper.GetString(1024),
			AccountNumber = RandomHelper.GetString(Regex.IBAN).RemoveWhitespace(),
			BankCode = RandomHelper.GetString(12),
			AmountEur = RandomHelper.GetInt(-500, 500),
			CreditorId = RandomHelper.GetString(),
			MandateReference = RandomHelper.GetString(64),
			CustomerReference = RandomHelper.GetString(64)
		};
		return transactionToReturn;
	}
}