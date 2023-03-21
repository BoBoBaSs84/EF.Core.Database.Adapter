using Application.Interfaces.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging;

/// <summary>
/// The microsoft logger wrapper class.
/// </summary>
/// <remarks>
/// Implements the <see cref="ILoggerWrapper{TLogger}"/> interface.
/// </remarks>
/// <typeparam name="TLogger"></typeparam>
public sealed class MicrosoftLoggerWrapper<TLogger> : ILoggerWrapper<TLogger> where TLogger : class
{
	private readonly ILogger<TLogger> _logger;

	/// <summary>
	/// Initializes an instance of the <see cref="MicrosoftLoggerWrapper{TLogger}"/> class.
	/// </summary>
	/// <param name="logger">The logger.</param>
	public MicrosoftLoggerWrapper(ILogger<TLogger> logger) =>
		_logger = logger;

	/// <inheritdoc/>
	public void Log(Action<ILogger, Exception?> del, Exception? exception = null) =>
		del?.Invoke(_logger, exception);

	/// <inheritdoc/>
	public void Log<T>(Action<ILogger, T, Exception?> del, T param, Exception? exception = null) =>
		del?.Invoke(_logger, param, exception);

	/// <inheritdoc/>
	public void Log<T1, T2>(Action<ILogger, T1, T2, Exception?> del, T1 param1, T2 param2, Exception? exception = null) =>
		del?.Invoke(_logger, param1, param2, exception);

	/// <inheritdoc/>
	public void Log<T1, T2, T3>(Action<ILogger, T1, T2, T3, Exception?> del,
		T1 param1, T2 param2, T3 param3, Exception? exception = null) =>
		del?.Invoke(_logger, param1, param2, param3, exception);

	/// <inheritdoc/>
	public void Log<T1, T2, T3, T4>(Action<ILogger, T1, T2, T3, T4, Exception?> del,
		T1 param1, T2 param2, T3 param3, T4 param4) =>
		del?.Invoke(_logger, param1, param2, param3, param4, null);

	/// <inheritdoc/>
	public void Log<T1, T2, T3, T4, T5>(Action<ILogger, T1, T2, T3, T4, T5, Exception?> del,
		T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) =>
		del?.Invoke(_logger, param1, param2, param3, param4, param5, null);

	/// <inheritdoc/>
	public void Log<T1, T2, T3, T4, T5, T6>(Action<ILogger, T1, T2, T3, T4, T5, T6, Exception?> del,
		T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) =>
		del?.Invoke(_logger, param1, param2, param3, param4, param5, param6, null);

	/// <inheritdoc/>
	[SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates")]
	[SuppressMessage("Usage", "CA2254:Template should be a static expression")]
	public void LogSlow(LogLevel level, string messageTemplate, params object[] args) =>
		_logger.Log(level, messageTemplate, args);

	/// <inheritdoc/>
	[SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates")]
	[SuppressMessage("Usage", "CA2254:Template should be a static expression")]
	public void LogExceptionSlow(Exception exception, string message) =>
		_logger.LogError(exception, message);
}
