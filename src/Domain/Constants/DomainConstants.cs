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
			/// The <see cref="COMMON"/> constant.
			/// </summary>
			public const string COMMON = "Common";
			/// <summary>
			/// The <see cref="ENUMERATOR"/> constant.
			/// </summary>
			public const string ENUMERATOR = "Enumerator";
			/// <summary>
			/// The <see cref="HISTORY"/> constant.
			/// </summary>
			public const string HISTORY = "History";
			/// <summary>
			/// The <see cref="IDENTITY"/> constant.
			/// </summary>
			public const string IDENTITY = "Identity";
			/// <summary>
			/// The <see cref="PRIVATE"/> constant.
			/// </summary>
			public const string PRIVATE = "Private";
			/// <summary>
			/// The <see cref="PRIVATE"/> constant.
			/// </summary>
			public const string FINANCE = "Finance";

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
		/// The method should return all the available xml namespaces.
		/// </summary>
		/// <returns>Serializable xml namespaces.</returns>
		public static XmlSerializerNamespaces GetNamespaces()
		{
			XmlSerializerNamespaces namespaces = new();
			namespaces.Add("ac", NameSpaces.ACTIVATABLE_NAMESPACE);
			namespaces.Add("ad", NameSpaces.AUDITED_NAMESPACE);
			namespaces.Add("at", NameSpaces.ATTENDANCE_NAMESPACE);
			namespaces.Add("cd", NameSpaces.CALENDARDAY_NAMESPACE);
			namespaces.Add("en", NameSpaces.ENUMERATOR_NAMSPACE);
			namespaces.Add("fi", NameSpaces.FINANCE_NAMSPACE);
			namespaces.Add("id", NameSpaces.IDENTITY_NAMESPACE);
			return namespaces;
		}

		/// <summary>
		/// The xml name space class.
		/// </summary>
		public static class NameSpaces
		{
			/// <summary>The <see cref="ACTIVATABLE_NAMESPACE"/> constant.</summary>
			public const string ACTIVATABLE_NAMESPACE = "http://DA.Models.org/Activatable";
			/// <summary>The <see cref="ACTIVATABLE_NAMESPACE"/> constant.</summary>
			public const string AUDITED_NAMESPACE = "http://DA.Models.org/Audited";
			/// <summary>The <see cref="ATTENDANCE_NAMESPACE"/> constant.</summary>
			public const string ATTENDANCE_NAMESPACE = "http://DA.Models.org/Attendance";
			/// <summary>The <see cref="CALENDARDAY_NAMESPACE"/> constant.</summary>
			public const string CALENDARDAY_NAMESPACE = "http://DA.Models.org/CalendarDay";
			/// <summary>The <see cref="ENUMERATOR_NAMSPACE"/> constant.</summary>
			public const string ENUMERATOR_NAMSPACE = "http://DA.Models.org/Enumerator";
			/// <summary>The <see cref="FINANCE_NAMSPACE"/> constant.</summary>
			public const string FINANCE_NAMSPACE = "http://DA.Models.org/Finance";
			/// <summary>The <see cref="IDENTITY_NAMESPACE"/> constant.</summary>
			public const string IDENTITY_NAMESPACE = "http://DA.Models.org/Identity";
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
