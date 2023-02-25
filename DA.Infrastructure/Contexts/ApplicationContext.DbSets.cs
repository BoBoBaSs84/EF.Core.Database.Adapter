using DA.Models.Contexts.Finances;
using DA.Models.Contexts.MasterData;
using DA.Models.Contexts.Timekeeping;
using Microsoft.EntityFrameworkCore;

namespace DA.Infrastructure.Contexts;

public sealed partial class ApplicationContext
{
	/// <inheritdoc/>
	public DbSet<CalendarDay> CalendarDays { get; set; } = default!;
	/// <inheritdoc/>
	public DbSet<CardType> CardTypes { get; set; } = default!;
	/// <inheritdoc/>
	public DbSet<DayType> DayTypes { get; set; } = default!;
	/// <inheritdoc/>
	public DbSet<Attendance> Attendances { get; set; } = default!;
	/// <inheritdoc/>
	public DbSet<Account> Accounts { get; set; } = default!;
	/// <inheritdoc/>
	public DbSet<Card> Cards { get; set; } = default!;
	/// <inheritdoc/>
	public DbSet<Transaction> Transactions { get; set; } = default!;
}
