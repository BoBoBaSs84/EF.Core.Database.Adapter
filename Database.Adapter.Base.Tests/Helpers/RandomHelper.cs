using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Database.Adapter.Base.Tests.Helpers;

[SuppressMessage("Style", "IDE0058", Justification = "UnitTestHelper")]
public static class RandomHelper
{
	private const string AlphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

	private static Random Random { get; } = new();
	public static byte[] GetBytes(int length = 8)
	{
		byte[] bytes = new byte[length];
		Random.NextBytes(bytes);
		return bytes;
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

	private static string RandomString(int length = 10)
	{
		StringBuilder stringBuilder = new();
		for (int i = 0; i < length; i++)
			stringBuilder.Append(AlphaNumeric[Random.Next(AlphaNumeric.Length)]);
		return stringBuilder.ToString();
	}
}
