namespace Database.Adapter.Entities;

/// <summary>
/// The constants class.
/// </summary>
public static class Constants
{
	/// <summary>
	/// The sql constants class.
	/// </summary>
	public static class SqlConstants
	{
		/// <summary>
		/// The sql schema class.
		/// </summary>
		public static class SqlSchema
		{
			/// <summary>The <see cref="APPLICATION"/> constant.</summary>
			public const string APPLICATION = "Application";
			/// <summary>The <see cref="IDENTITY"/> constant.</summary>
			public const string IDENTITY = "Identity";
			/// <summary>The <see cref="PRIVATE"/> constant.</summary>
			public const string PRIVATE = "Private";
			/// <summary>The <see cref="HISTORY"/> constant.</summary>
			public const string HISTORY = "History";
		}
		/// <summary>
		/// The sql data type class.
		/// </summary>
		public static class SqlDataType
		{
			/// <summary>The <see cref="DATETIME"/> constant.</summary>
			public const string DATETIME = "datetime";
			/// <summary>The <see cref="DATE"/> constant.</summary>
			public const string DATE = "date";
			/// <summary>The <see cref="TIME"/> constant.</summary>
			public const string TIME = "time(7)";
			/// <summary>The <see cref="XML"/> constant.</summary>
			public const string XML = "xml";
			/// <summary>The <see cref="FLOAT"/> constant.</summary>
			public const string FLOAT = "float";
			/// <summary>The <see cref="MONEY"/> constant.</summary>
			public const string MONEY = "money";
		}
		/// <summary>
		/// The sql string length class.
		/// </summary>
		public static class SqlStringLength
		{
			/// <summary>The <see cref="MAX_LENGHT_32"/> constant.</summary>
			public const int MAX_LENGHT_32 = 32;
			/// <summary>The <see cref="MAX_LENGHT_64"/> constant.</summary>
			public const int MAX_LENGHT_64 = 64;
			/// <summary>The <see cref="MAX_LENGHT_128"/> constant.</summary>
			public const int MAX_LENGHT_128 = 128;
			/// <summary>The <see cref="MAX_LENGHT_256"/> constant.</summary>
			public const int MAX_LENGHT_256 = 256;
			/// <summary>The <see cref="MAX_LENGHT_512"/> constant.</summary>
			public const int MAX_LENGHT_512 = 512;
			/// <summary>The <see cref="MAX_LENGHT_1024"/> constant.</summary>
			public const int MAX_LENGHT_1024 = 1024;
			/// <summary>The <see cref="MAX_LENGHT_2048"/> constant.</summary>
			public const int MAX_LENGHT_2048 = 2048;
			/// <summary>The <see cref="MAX_LENGHT_4092"/> constant.</summary>
			public const int MAX_LENGHT_4092 = 4092;
		}
	}
}
