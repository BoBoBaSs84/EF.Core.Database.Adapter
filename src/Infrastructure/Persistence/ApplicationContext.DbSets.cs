using Domain.Entities.Enumerator;
using Domain.Entities.Finance;
using Domain.Entities.Private;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed partial class ApplicationContext
{
	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="CalendarDay"/>.
	/// </summary>
	public DbSet<CalendarDay> CalendarDays { get; set; } = default!;

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="CardType"/>.
	/// </summary>
	public DbSet<CardType> CardTypes { get; set; } = default!;

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="DayType"/>.
	/// </summary>
	public DbSet<DayType> DayTypes { get; set; } = default!;

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="Attendance"/>.
	/// </summary>
	public DbSet<Attendance> Attendances { get; set; } = default!;

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="Account"/>.
	/// </summary>
	public DbSet<Account> Accounts { get; set; } = default!;

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="Card"/>.
	/// </summary>
	public DbSet<Card> Cards { get; set; } = default!;

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="Transaction"/>.
	/// </summary>
	public DbSet<Transaction> Transactions { get; set; } = default!;
}
