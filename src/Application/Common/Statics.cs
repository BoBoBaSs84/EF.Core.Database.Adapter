namespace Application.Common;

internal static class Statics
{
	public static class DateRanges
	{
		public const int MinYear = 1970;
		public const int MaxYear = 2069;
		public const int MinMonth = 1;
		public const int MaxMonth = 12;
		public const int MinDay = 1;
		public const int MaxDay = 12;
		public static readonly DateTime MinDate = new(MinYear, MinMonth, MinDay);
		public static readonly DateTime MaxDate = new(MaxYear, MaxMonth, MaxDay);
	}
}
