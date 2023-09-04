using System.Globalization;
using System.Text;

namespace Domain.Extensions;

/// <summary>
/// The byte extensions class.
/// </summary>
public static class ByteExtensions
{
	/// <summary>
	/// Gets the hex string of a byte array
	/// </summary>
	/// <param name="inputBuffer">The byte array to work with.</param>
	/// <returns>The hexed string</returns>
	public static string GetHexString(this byte[] inputBuffer)
	{
		StringBuilder sb = new();
		foreach (byte b in inputBuffer)
			_ = sb.Append(b.ToString("X2", CultureInfo.InvariantCulture));

		return sb.ToString();
	}
}
