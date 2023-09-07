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
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="AccountModel"/>.
	/// </summary>
	public DbSet<AccountModel> Accounts { get; set; } = default!;

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="CardModel"/>.
	/// </summary>
	public DbSet<CardModel> Cards { get; set; } = default!;

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="TransactionModel"/>.
	/// </summary>
	public DbSet<TransactionModel> Transactions { get; set; } = default!;
}
