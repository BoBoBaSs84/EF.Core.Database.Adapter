using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Entities.Contexts.Timekeeping;
using static Database.Adapter.Entities.Constants;
using static Database.Adapter.Entities.Statics;

namespace Database.Adapter.Base.Tests.Helpers;

public static class EntityHelper
{
	public static User GetNewUser() => new()
	{
		FirstName = RandomHelper.GetString(64),
		LastName = RandomHelper.GetString(64),
		Email = "UnitTest@Test.org",
		NormalizedEmail = "UNITTEST@TEST.ORG",
		UserName = Constants.UnitTestUserName,
		NormalizedUserName = Constants.UnitTestUserName.ToUpper(CurrentCulture),
	};

	public static ICollection<Attendance> GetNewAttendanceCollection(User user, CalendarDay calendarDay, DayType dayType) =>
		new List<Attendance>() { new Attendance() { User = user, CalendarDay = calendarDay, DayType = dayType } };

	public static Account GetNewAccount(string? iban = null) =>
		new() { IBAN = iban ?? RandomHelper.GetString(Regex.IBAN), Provider = RandomHelper.GetString(128) };

	public static ICollection<AccountUser> GetNewAccountUserColection(User user, Account account) =>
		new List<AccountUser>() { new AccountUser() { User = user, Account = account } };
}