using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Models.Attendance;
using Domain.Models.Common;
using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces.Persistence;

/// <summary>
/// The application context interface.
/// </summary>
public interface IRepositoryContext : IDbContext
{
	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="AccountModel"/>.
	/// </summary>
	DbSet<AccountModel> Accounts { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="AttendanceModel"/>.
	/// </summary>
	DbSet<AttendanceModel> Attendances { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="CalendarModel"/>.
	/// </summary>
	DbSet<CalendarModel> CalendarDays { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="CardModel"/>.
	/// </summary>
	DbSet<CardModel> Cards { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="TransactionModel"/>.
	/// </summary>
	DbSet<TransactionModel> Transactions { get; }
}
