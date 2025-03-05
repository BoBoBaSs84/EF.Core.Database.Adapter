using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Entities.Documents;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Entities.Todo;

using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Infrastructure.Persistence;

/// <summary>
/// The application context interface.
/// </summary>
public interface IRepositoryContext : IDbContext
{
	/// <inheritdoc cref="DbSet{TEntity}"/>
	DbSet<AccountEntity> Accounts { get; }

	/// <inheritdoc cref="DbSet{TEntity}"/>
	DbSet<AttendanceEntity> Attendances { get; }

	/// <inheritdoc cref="DbSet{TEntity}"/>
	DbSet<CardEntity> Cards { get; }

	/// <inheritdoc cref="DbSet{TEntity}"/>
	DbSet<DocumentEntity> Documents { get; }

	/// <inheritdoc cref="DbSet{TEntity}"/>
	DbSet<TransactionEntity> Transactions { get; }

	/// <inheritdoc cref="DbSet{TEntity}"/>
	DbSet<ListEntity> TodoLists { get; }

	/// <inheritdoc cref="DbSet{TEntity}"/>
	DbSet<ItemEntity> TodoItems { get; }

	/// <summary>
	/// This function returns the last day of the month containing a specified date.
	/// </summary>
	/// <param name="inputValue">A date expression that specifies the date for which to return the last day of the month.</param>
	DateTime EndOfMonth(DateTime inputValue);

	/// <summary>
	/// This function returns a Unicode string with the delimiters added to make the
	/// input string a valid SQL Server delimited identifier.
	/// </summary>
	/// <remarks>
	/// Returns <see langword="null"/> if <paramref name="inputValue"/> is greater than 128 characters.
	/// </remarks>
	/// <param name="inputValue">Is a string of Unicode character data.</param>
	string? QuoteName(string inputValue);

	/// <summary>
	/// Converts an alphanumeric string to a four-character code that is based on how the string
	/// sounds when spoken in English.
	/// </summary>
	/// <param name="inputValue">Is an alphanumeric expression of character data.</param>
	string Soundex(string inputValue);

	/// <summary>
	/// Returns an integer value measuring the difference between the <see cref="Soundex(string)"/> values
	/// of two different character expressions.
	/// </summary>
	/// <param name="inputValue">An alphanumeric expression of character data.</param>
	/// <param name="valueToCompare">An alphanumeric expression of character data.</param>
	/// <returns></returns>
	int Difference(string inputValue, string valueToCompare);

	/// <summary>
	/// Returns the string provided as a first argument, after some characters specified in the second argument
	/// are translated guido a destination set of characters, specified in the third argument.
	/// </summary>
	/// <param name="inputValue">The string expression to be searched.</param>
	/// <param name="characters">A string expression containing characters that should be replaced.</param>
	/// <param name="translations">A string expression containing the replacement characters.</param>
	string Translate(string inputValue, string characters, string translations);
}
