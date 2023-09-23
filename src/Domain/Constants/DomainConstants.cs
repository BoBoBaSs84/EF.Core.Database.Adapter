using System.Xml.Serialization;

namespace Domain.Constants;

/// <summary>
/// The constants class.
/// </summary>
[SuppressMessage("Naming", "CA1720", Justification = "Constants.")]
[SuppressMessage("Naming", "CA1707", Justification = "Constants.")]
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
	/// The sql constants class.
	/// </summary>
	public static class Sql
	{
		/// <summary>
		/// The db function class.
		/// </summary>
		public static class DbFunction
		{
			/// <summary>
			/// The end of month function.
			/// </summary>
			public const string ENDOFMONTH = "EOMONTH";
			/// <summary>
			/// The sound ex function.
			/// </summary>
			public const string SOUNDLIKE = "SOUNDEX";
			/// <summary>
			/// The translate function.
			/// </summary>
			public const string TRANSLATE = "TRANSLATE";
			/// <summary>
			/// The quote functions.
			/// </summary>
			public const string QUOTENAME = "QUOTENAME";
		}

		/// <summary>
		/// The sql schema class.
		/// </summary>
		public static class Schema
		{
			/// <summary>
			/// The <see cref="Attendance"/> constant.
			/// </summary>
			public const string Attendance = "Attendance";
			/// <summary>
			/// The <see cref="Common"/> constant.
			/// </summary>
			public const string Common = "Common";
			/// <summary>
			/// The <see cref="History"/> constant.
			/// </summary>
			public const string History = "History";
			/// <summary>
			/// The <see cref="Identity"/> constant.
			/// </summary>
			public const string Identity = "Identity";
			/// <summary>
			/// The <see cref="Migration"/> constant.
			/// </summary>
			public const string Migration = "Migration";
			/// <summary>
			/// The <see cref="Finance"/> constant.
			/// </summary>
			public const string Finance = "Finance";

		}
		/// <summary>
		/// The sql data type class.
		/// </summary>
		public static class DataType
		{
			/// <summary>The <see cref="DATETIME"/> constant.</summary>
			public const string DATETIME = "datetime";
			/// <summary>The <see cref="DATETIME2"/> constant.</summary>
			public const string DATETIME2 = "datetime2";
			/// <summary>The <see cref="DATE"/> constant.</summary>
			public const string DATE = "date";
			/// <summary>The <see cref="TIME0"/> constant.</summary>
			public const string TIME0 = "time(0)";
			/// <summary>The <see cref="TIME7"/> constant.</summary>
			public const string TIME7 = "time(7)";
			/// <summary>The <see cref="XML"/> constant.</summary>
			public const string XML = "xml";
			/// <summary>The <see cref="FLOAT"/> constant.</summary>
			public const string FLOAT = "float";
			/// <summary>The <see cref="MONEY"/> constant.</summary>
			public const string MONEY = "money";
		}

		/// <summary>
		/// The sql max length class.
		/// </summary>
		public static class MaxLength
		{
			/// <summary>The <see cref="MAX_25"/> constant.</summary>
			public const int MAX_25 = 25;
			/// <summary>The <see cref="MAX_50"/> constant.</summary>
			public const int MAX_50 = 50;
			/// <summary>The <see cref="MAX_100"/> constant.</summary>
			public const int MAX_100 = 100;
			/// <summary>The <see cref="MAX_250"/> constant.</summary>
			public const int MAX_250 = 250;
			/// <summary>The <see cref="MAX_500"/> constant.</summary>
			public const int MAX_500 = 500;
			/// <summary>The <see cref="MAX_1000"/> constant.</summary>
			public const int MAX_1000 = 1000;
			/// <summary>The <see cref="MAX_2000"/> constant.</summary>
			public const int MAX_2000 = 2000;
			/// <summary>The <see cref="MAX_4000"/> constant.</summary>
			public const int MAX_4000 = 4000;
		}
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

	/// <summary>
	/// The regex constants class.
	/// </summary>
	public static class RegexPatterns
	{
		/// <summary>
		/// The email regex pattern.
		/// </summary>
		public const string Email = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

		/// <summary>
		/// The <see cref="IBAN"/> property.
		/// </summary>
		/// <remarks>
		/// The International Bank Account Number
		/// </remarks>
		public const string IBAN = @"^[A-Z]{2}(?:[ ]?[0-9]){18,20}$";

		/// <summary>
		/// The <see cref="CC"/> property.
		/// </summary>
		/// <remarks>
		/// Credit card number is the card unique identifier found on payment cards.
		/// </remarks>
		public const string CC = @"(^4[0-9]{12}(?:[0-9]{3})?$)|(^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$)|(3[47][0-9]{13})|(^3(?:0[0-5]|[68][0-9])[0-9]{11}$)|(^6(?:011|5[0-9]{2})[0-9]{12}$)|(^(?:2131|1800|35\d{3})\d{11}$)";

		/// <summary>
		/// The whitespace regex pattern.
		/// </summary>
		public const string Whitespace = @"\s+";
	}
}
