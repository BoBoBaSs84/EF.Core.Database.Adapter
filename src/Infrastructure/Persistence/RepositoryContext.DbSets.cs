using Domain.Models.Attendance;
using Domain.Models.Common;
using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed partial class RepositoryContext
{
	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="CalendarModel"/>.
	/// </summary>
	public DbSet<CalendarModel> CalendarDays { get; set; } = default!;

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="AttendanceModel"/>.
	/// </summary>
	public DbSet<AttendanceModel> Attendances { get; set; } = default!;

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
