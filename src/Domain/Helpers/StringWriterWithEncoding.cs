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
	private readonly Encoding? _encoding;

	/// <summary>
	/// Overrides the default encoding type (UTF-16).
	/// </summary>
	/// <inheritdoc/>
	public override Encoding Encoding
		=> _encoding ?? base.Encoding;

	/// <summary>
	/// Initializes a new instance of the string writer with encoding class.
	/// </summary>
	/// <remarks>
	/// The <see cref="StringWriter.Encoding"/> is used.
	/// </remarks>
	internal StringWriterWithEncoding() : base()
	{ }

	/// <summary>
	/// Initializes a new instance of the string writer with encoding class.
	/// </summary>
	/// <param name="encoding">The character encoding type to use.</param>
	internal StringWriterWithEncoding(Encoding encoding) : base()
		=> _encoding = encoding;
}
