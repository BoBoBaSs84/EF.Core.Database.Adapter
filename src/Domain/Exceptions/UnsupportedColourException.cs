namespace Domain.Exceptions;

/// <summary>
/// The unsupported colour exception class.
/// </summary>
/// <param name="code">The color code that is unsupported.</param>
public sealed class UnsupportedColourException(string code) : Exception($"Colour \"{code}\" is unsupported.")
{ }