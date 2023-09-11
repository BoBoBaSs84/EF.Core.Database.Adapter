﻿using Application.Interfaces.Infrastructure.Logging;

using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging;

/// <summary>
/// The logger service class.
/// </summary>
/// <remarks>
/// Implements the <see cref="ILoggerService{TLogger}"/> interface.
/// </remarks>
/// <typeparam name="TLogger"></typeparam>
public sealed class LoggerService<TLogger> : ILoggerService<TLogger> where TLogger : class
{
	private readonly ILogger<TLogger> _logger;

	/// <summary>
	/// Initializes an instance of the <see cref="LoggerService{TLogger}"/> class.
	/// </summary>
	/// <param name="logger">The logger.</param>
	public LoggerService(ILogger<TLogger> logger)
		=> _logger = logger;

	/// <inheritdoc/>
	public void Log(Action<ILogger, Exception?> del, Exception? exception = null)
		=> del?.Invoke(_logger, exception);

	/// <inheritdoc/>
	public void Log<T>(Action<ILogger, T, Exception?> del, T param, Exception? exception = null)
		=> del?.Invoke(_logger, param, exception);

	/// <inheritdoc/>
	public void Log<T1, T2>(Action<ILogger, T1, T2, Exception?> del, T1 param1, T2 param2, Exception? exception = null)
		=> del?.Invoke(_logger, param1, param2, exception);

	/// <inheritdoc/>
	public void Log<T1, T2, T3>(Action<ILogger, T1, T2, T3, Exception?> del,
		T1 param1, T2 param2, T3 param3, Exception? exception = null)
		=> del?.Invoke(_logger, param1, param2, param3, exception);

	/// <inheritdoc/>
	public void Log<T1, T2, T3, T4>(Action<ILogger, T1, T2, T3, T4, Exception?> del,
		T1 param1, T2 param2, T3 param3, T4 param4)
		=> del?.Invoke(_logger, param1, param2, param3, param4, null);

	/// <inheritdoc/>
	public void Log<T1, T2, T3, T4, T5>(Action<ILogger, T1, T2, T3, T4, T5, Exception?> del,
		T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
		=> del?.Invoke(_logger, param1, param2, param3, param4, param5, null);

	/// <inheritdoc/>
	public void Log<T1, T2, T3, T4, T5, T6>(Action<ILogger, T1, T2, T3, T4, T5, T6, Exception?> del,
		T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
		=> del?.Invoke(_logger, param1, param2, param3, param4, param5, param6, null);
}