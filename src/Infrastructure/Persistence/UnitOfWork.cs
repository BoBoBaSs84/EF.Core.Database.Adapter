using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence;

/// <summary>
/// The unit of work class.
/// </summary>
/// <remarks>
/// Implemnts the members of the <see cref="IUnitOfWork"/> interface.
/// </remarks>
public abstract class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationContext _context;
	private bool _disposed;

	private readonly Lazy<IAccountRepository> lazyAccountRepository;
	private readonly Lazy<ICardRepository> lazyCardRepository;
	private readonly Lazy<ITransactionRepository> lazyTransactionRepository;
	private readonly Lazy<ICalendarDayRepository> lazyCalendarRepository;
	private readonly Lazy<IDayTypeRepository> lazyDayTypeRepository;
	private readonly Lazy<ICardTypeRepository> lazyCardTypeRepository;
	private readonly Lazy<IAttendanceRepository> lazyAttendanceRepository;

	/// <summary>
	/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
	/// </summary>
	/// <param name="context">The database context to work with.</param>
	public UnitOfWork(ApplicationContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));

		lazyAccountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(_context));
		lazyCardRepository = new Lazy<ICardRepository>(() => new CardRepository(_context));
		lazyTransactionRepository = new Lazy<ITransactionRepository>(() => new TransactionRepository(_context));
		lazyCalendarRepository = new Lazy<ICalendarDayRepository>(() => new CalendarDayRepository(_context));
		lazyDayTypeRepository = new Lazy<IDayTypeRepository>(() => new DayTypeRepository(_context));
		lazyCardTypeRepository = new Lazy<ICardTypeRepository>(() => new CardTypeRepository(_context));
		lazyAttendanceRepository = new Lazy<IAttendanceRepository>(() => new AttendanceRepository(_context));
	}

	/// <inheritdoc/>
	public IAccountRepository AccountRepository => lazyAccountRepository.Value;

	/// <inheritdoc/>
	public IAttendanceRepository AttendanceRepository => lazyAttendanceRepository.Value;

	/// <inheritdoc/>
	public ICalendarDayRepository CalendarDayRepository => lazyCalendarRepository.Value;

	/// <inheritdoc/>
	public ICardRepository CardRepository => lazyCardRepository.Value;

	/// <inheritdoc/>
	public ICardTypeRepository CardTypeRepository => lazyCardTypeRepository.Value;

	/// <inheritdoc/>
	public IDayTypeRepository DayTypeRepository => lazyDayTypeRepository.Value;

	/// <inheritdoc/>
	public ITransactionRepository TransactionRepository => lazyTransactionRepository.Value;

	/// <inheritdoc/>
	public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default) =>
		await _context.SaveChangesAsync(cancellationToken);

	/// <inheritdoc/>
	public async ValueTask DisposeAsync()
	{
		await DisposeAsync(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Cleans up any resources being used.
	/// </summary>
	/// <param name="disposing">Whether or not we are disposing</param> 
	/// <returns><see cref="ValueTask"/></returns>
	protected virtual async ValueTask DisposeAsync(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
				// Dispose managed resources.
				await _context.DisposeAsync();
			// Dispose any unmanaged resources here...
			_disposed = true;
		}
	}
}
