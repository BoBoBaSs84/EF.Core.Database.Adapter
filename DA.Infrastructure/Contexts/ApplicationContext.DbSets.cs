using DA.Domain.Models.Finances;
using DA.Domain.Models.MasterData;
using DA.Domain.Models.Timekeeping;
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
