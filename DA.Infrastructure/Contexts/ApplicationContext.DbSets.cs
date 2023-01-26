using DA.Models.Contexts.Finances;
using DA.Models.Contexts.MasterData;
using DA.Models.Contexts.Timekeeping;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.Contexts;

public sealed partial class ApplicationContext
{
	/// <summary>
	/// The <see cref="CalendarDays"/> property.
	/// </summary>
	public DbSet<CalendarDay> CalendarDays { get; set; } = default!;
	/// <summary>
	/// The <see cref="CardTypes"/> property.
	/// </summary>
	public DbSet<CardType> CardTypes { get; set; } = default!;
	/// <summary>
	/// The <see cref="DayTypes"/> property.
	/// </summary>
	public DbSet<DayType> DayTypes { get; set; } = default!;
	/// <summary>
	/// The <see cref="Attendances"/> property.
	/// </summary>
	public DbSet<Attendance> Attendances { get; set; } = default!;
	/// <summary>
	/// The <see cref="Accounts"/> property.
	/// </summary>
	public DbSet<Account> Accounts { get; set; } = default!;
	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public DbSet<Card> Cards { get; set; } = default!;
	/// <summary>
	/// The <see cref="Transactions"/> property.
	/// </summary>
	public DbSet<Transaction> Transactions { get; set; } = default!;
}
