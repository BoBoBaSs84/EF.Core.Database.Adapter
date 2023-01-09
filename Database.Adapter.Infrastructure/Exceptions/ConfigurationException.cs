namespace Database.Adapter.Infrastructure.Exceptions;

/// <summary>
/// The configuration exception class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="Exception"/> class.
/// </remarks>
internal sealed class ConfigurationException : Exception
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigurationException"/> class.
	/// </summary>
	/// <param name="message">The exception message.</param>
	public ConfigurationException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigurationException"/> class.
	/// </summary>
	/// <param name="message">The exception message.</param>
	/// <param name="innerException">The inner exception.</param>
	public ConfigurationException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
