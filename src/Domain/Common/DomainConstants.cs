using System.Xml.Serialization;

namespace Domain.Common;

/// <summary>
/// The constants class.
/// </summary>
public static class DomainConstants
{
	/// <summary>
	/// The environment constants class.
	/// </summary>
	public static class Environment
	{
		/// <summary>
		/// The development environment.
		/// </summary>
		public const string Development = "Development";
		/// <summary>
		/// The test environment.
		/// </summary>
		public const string Testing = "Testing";
		/// <summary>
		/// The production environment.
		/// </summary>
		public const string Production = "Production";
	}

	/// <summary>
	/// The xml constants class.
	/// </summary>
	public static class Xml
	{
		/// <summary>
		/// The xml elements class.
		/// </summary>
		public static class Elements
		{
			/// <summary>
			/// The attendance element.
			/// </summary>
			public const string Attendance = "Attendance";

			/// <summary>
			/// The preferences element.
			/// </summary>
			public const string Preferences = "Preferences";

			/// <summary>
			/// The work days element.
			/// </summary>
			public const string WorkDays = "WorkDays";

			/// <summary>
			/// The work hours element.
			/// </summary>
			public const string WorkHours = "WorkHours";

			/// <summary>
			/// The vacation days element.
			/// </summary>
			public const string VacationDays = "VacationDays";
		}

		/// <summary>
		/// The method should return all the available xml namespaces.
		/// </summary>
		/// <returns>Serializable xml namespaces.</returns>
		public static XmlSerializerNamespaces GetNamespaces()
		{
			XmlSerializerNamespaces namespaces = new();
			namespaces.Add(string.Empty, NameSpaces.Preferences);
			return namespaces;
		}

		/// <summary>
		/// The xml name space class.
		/// </summary>
		public static class NameSpaces
		{
			/// <summary>
			/// The preferences xml namespace.
			/// </summary>
			public const string Preferences = "https://github.com/BoBoBaSs84/EF.Core.Database.Adapter/XmlSchema/Preferences";
		}

		/// <summary>
		/// The xml data type class.
		/// </summary>
		public static class DataType
		{
			/// <summary>The <see cref="BOOL"/> constant.</summary>
			public const string BOOL = "boolean";
			/// <summary>The <see cref="BYTE"/> constant.</summary>
			public const string BYTE = "unsignedByte";
			/// <summary>The <see cref="BYTEARRAY"/> constant.</summary>
			public const string BYTEARRAY = "hexBinary";
			/// <summary>The <see cref="DATETIME"/> constant.</summary>
			public const string DATETIME = "dateTime";
			/// <summary>The <see cref="DATE"/> constant.</summary>
			public const string DATE = "date";
			/// <summary>The <see cref="FLOAT"/> constant.</summary>
			public const string FLOAT = "float";
			/// <summary>The <see cref="TIME"/> constant.</summary>
			public const string TIME = "time";
			/// <summary>The <see cref="STRING"/> constant.</summary>
			public const string STRING = "string";
			/// <summary>The <see cref="INT"/> constant.</summary>
			public const string INT = "int";
			/// <summary>The <see cref="ID"/> constant.</summary>
			public const string ID = "ID";
		}
	}
}
