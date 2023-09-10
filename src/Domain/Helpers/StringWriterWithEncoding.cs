using System.Text;

namespace Domain.Helpers;

/// <summary>
/// The string writer with encoding class.
/// </summary>
/// <remarks>
/// Overrides the base <see cref="StringWriter"/> class to accept a different character encoding type.
/// </remarks>
internal sealed class StringWriterWithEncoding : StringWriter
{
	private readonly Encoding _encoding;

	/// <summary>
	/// Overrides the default encoding type (UTF-16).
	/// </summary>
	/// <inheritdoc/>
	public override Encoding Encoding
		=> _encoding;

	/// <summary>
	/// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
	/// </summary>
	public StringWriterWithEncoding()
		=> _encoding = Encoding.Unicode;

	/// <summary>
	/// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
	/// </summary>
	/// <param name="encoding">The character encoding type</param>
	public StringWriterWithEncoding(Encoding encoding)
		=> _encoding = encoding;
}
