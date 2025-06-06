﻿using System.Text;

using Fare;

using static BB84.Home.Base.Tests.Constants.TestConstants;

namespace BB84.Home.Base.Tests.Helpers;

[SuppressMessage("Style", "IDE0058", Justification = "UnitTestHelper")]
public static class RandomHelper
{
	private static Random Random { get; } = new();

	public static byte[] GetBytes(int length = 8)
	{
		byte[] bytes = new byte[length];
		Random.NextBytes(bytes);
		return bytes;
	}

	public static DateTime GetDateTime(DateTime? minDate = null, DateTime? maxDate = null)
	{
		minDate ??= DateTime.MinValue;
		maxDate ??= DateTime.MaxValue;

		int range = (maxDate - minDate).Value.Days;
		return minDate.Value.AddDays(Random.Next(range));
	}

	public static DateTime GetDateTime(int year)
	{
		DateTime startDate = new(year, 1, 1);
		DateTime endDate = startDate.AddYears(1).AddDays(-1);
		int range = (endDate - startDate).Days;
		return startDate.AddDays(Random.Next(range));
	}

	public static decimal GetDecimal()
	{
		byte scale = (byte)Random.Next(29);
		bool sign = Random.Next(2).Equals(1);
		return new decimal(Random.Next(), Random.Next(), Random.Next(), sign, scale);
	}

	public static double GetDouble() => Random.NextDouble();

	public static float GetFloat() => Random.NextSingle();

	public static int GetInt() => Random.Next();

	public static int GetInt(int max) => Random.Next(max);

	public static int GetInt(int min, int max) => Random.Next(min, max);

	public static long GetLong() => Random.NextInt64();

	public static long GetLong(long max) => Random.NextInt64(max);

	public static long GetLong(long min, long max) => Random.NextInt64(min, max);

	public static string GetString() => RandomString();

	public static string GetString(int maxChars) => RandomString(maxChars);

	public static string GetString(int maxChars, string pattern) => RandomString(maxChars, pattern);

	public static string GetString(string regexPattern)
	{
		Xeger xeger = new(regexPattern, Random);
		return xeger.Generate();
	}

	private static string RandomString(int length = 10, string pattern = CharsOnly)
	{
		StringBuilder stringBuilder = new();
		for (int i = 0; i < length; i++)
			stringBuilder.Append(pattern[Random.Next(pattern.Length)]);
		return stringBuilder.ToString();
	}
}
