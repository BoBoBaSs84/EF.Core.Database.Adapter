using DA.Domain.Models.MasterData;
using DA.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;

namespace DA.InfrastructureTests.Data;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest - Not relevant here.")]
public class ApplicationContextTests : InfrastructureBaseTests
{
	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
	{
		ApplicationContext appContext = GetRequiredService<ApplicationContext>();
		appContext.Database.EnsureDeleted();
	}

	[TestMethod, Owner(Bobo)]
	public void MigrationsTest()
	{
		ApplicationContext appContext = GetRequiredService<ApplicationContext>();		
		appContext.Database.Migrate();

		ICollection<CalendarDay> calendarDays = GetCalendarDays();

		appContext.CalendarDays.AddRange(calendarDays);
		int commit = appContext.SaveChanges();

		commit.Should().Be(calendarDays.Count);
	}

	[ClassCleanup] 
	public static void ClassCleanup()
	{
		ApplicationContext appContext = GetRequiredService<ApplicationContext>();
		appContext.Database.EnsureCreated();
	}

	private static ICollection<CalendarDay> GetCalendarDays()
	{
		ICollection<CalendarDay> calendarDays = new List<CalendarDay>();
		DateTime startDate = new(1900, 1, 1), endDate = new(2100, 1, 1);
		while (startDate <= endDate)
		{
			CalendarDay calendarDay = new()
			{
				Date = startDate,
				DayTypeId = (startDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday) ? 2 : 1
			};
			calendarDays.Add(calendarDay);
			startDate = startDate.AddDays(1);
		}
		return calendarDays;
	}
}