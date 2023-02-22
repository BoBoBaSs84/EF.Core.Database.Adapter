using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DA.Models.Extensions;

/// <summary>
/// The XML extension class.
/// </summary>
public static class XmlExtension
{
	/// <summary>
	/// The standard XML writer settings.
	/// </summary>
	public static XmlWriterSettings WriterSettings => new()
	{
		Indent = true,
		CheckCharacters = true,
		OmitXmlDeclaration = true,
		NamespaceHandling = NamespaceHandling.OmitDuplicates,
		ConformanceLevel = ConformanceLevel.Document
	};

	/// <summary>
	/// Converts an object to its serialized XML format.
	/// </summary>
	/// <typeparam name="T">The type of object we are operating on.</typeparam>
	/// <param name="value">The object we are operating on.</param>
	/// <param name="namespaces">The xml namespace to use.</param>
	/// <param name="settings">The xml writer settings to use.</param>
	/// <param name="encoding">The character encoding to use.</param>
	/// <returns>The XML string representation of the object <typeparamref name="T"/>.</returns>
	public static string ToXmlString<T>(this T value, XmlSerializerNamespaces? namespaces = null, XmlWriterSettings? settings = null, Encoding? encoding = null) where T : class
	{
		namespaces ??= new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

		settings ??= new()
		{
			Indent = true,
			OmitXmlDeclaration = true,
			CheckCharacters = false
		};

		encoding ??= Encoding.UTF8;

		using StringWriterWithEncoding stream = new(encoding);
		using XmlWriter writer = XmlWriter.Create(stream, settings);
		XmlSerializer serializer = new(value.GetType());
		serializer.Serialize(writer, value, namespaces);
		return stream.ToString();
	}

	/// <summary>
	/// Creates an object instance from the specified XML string.
	/// </summary>
	/// <typeparam name="T">The Type of the object we are operating on</typeparam>
	/// <param name="value">The object we are operating on</param>
	/// <param name="xmlString">The XML string to deserialize from</param>
	/// <returns>An object instance</returns>
	public static T FromXmlString<T>(this T value, string xmlString) where T : class
	{
		using XmlReader xmlReader= XmlReader.Create(xmlString);
		XmlSerializer serializer = new(value.GetType());
		return (T)serializer.Deserialize(xmlReader)!;
	}

	/// <summary>
	/// Overrides the base 'StringWriter' class to accept a different character encoding type.
	/// </summary>
	private class StringWriterWithEncoding : StringWriter
	{
		private readonly Encoding _encoding = default!;

		/// <summary>
		/// Overrides the default encoding type (UTF-16).
		/// </summary>
		public override Encoding Encoding => _encoding ?? base.Encoding;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public StringWriterWithEncoding() { }

		/// <summary>
		/// Constructor which accepts a character encoding type.
		/// </summary>
		/// <param name="encoding">The character encoding type</param>
		public StringWriterWithEncoding(Encoding encoding) => _encoding = encoding;
	}
}
