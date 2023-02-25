using DA.Models.Contexts.Finances;
using DA.Models.Contexts.MasterData;
using DA.Models.Contexts.Timekeeping;
using Microsoft.EntityFrameworkCore;

namespace DA.Infrastructure.Contexts.Interfaces;

/// <summary>
/// The application context interface.
/// </summary>
public interface IApplicationContext
{
	/// <summary>
	/// The <see cref="CalendarDays"/> property.
	/// </summary>
	DbSet<CalendarDay> CalendarDays { get; }
	/// <summary>
	/// The <see cref="CardTypes"/> property.
	/// </summary>
	DbSet<CardType> CardTypes { get; }
	/// <summary>
	/// The <see cref="DayTypes"/> property.
	/// </summary>
	DbSet<DayType> DayTypes { get; }
	/// <summary>
	/// The <see cref="Attendances"/> property.
	/// </summary>
	DbSet<Attendance> Attendances { get; }
	/// <summary>
	/// The <see cref="Accounts"/> property.
	/// </summary>
	DbSet<Account> Accounts { get; }
	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	DbSet<Card> Cards { get; }
	/// <summary>
	/// The <see cref="Transactions"/> property.
	/// </summary>
	DbSet<Transaction> Transactions { get; }
}
