using System.Globalization;
using System.Reflection;

namespace DA.Infrastructure;

internal static class Statics
{
	private static readonly Assembly assembly;
	/// <summary>
	/// The <see cref="AppDomainName"/> property.
	/// </summary>
	public static string AppDomainName { get; private set; }
	/// <summary>
	/// The <see cref="AssemblyName"/> property.
	/// </summary>
	public static string AssemblyName { get; private set; }
	/// <summary>
	/// The <see cref="AssemblyVersion"/> property.
	/// </summary>
	public static string AssemblyVersion { get; private set; }
	/// <summary>
	/// The <see cref="BaseDirectory"/> property.
	/// </summary>
	public static string BaseDirectory { get; private set; }
	/// <summary>
	/// The <see cref="ConfigurationFileName"/> property.
	/// </summary>
	public static string ConfigurationFileName { get; private set; }
	/// <summary>
	/// The <see cref="CurrentCulture"/> property.
	/// </summary>
	public static CultureInfo CurrentCulture { get; private set; }
	/// <summary>
	/// The <see cref="MachineName"/> property.
	/// </summary>
	public static string MachineName { get; private set; }
	/// <summary>
	/// The <see cref="UserName"/> property.
	/// </summary>
	public static string UserName { get; private set; }
	/// <summary>
	/// The static <see cref="Statics"/> class constructor.
	/// </summary>
	static Statics()
	{
		AppDomainName = AppDomain.CurrentDomain.FriendlyName;
		assembly = Assembly.GetExecutingAssembly();
		AssemblyName = assembly.GetName().Name!;
		AssemblyVersion = assembly.GetName().Version!.ToString();
		BaseDirectory = AppContext.BaseDirectory;
		ConfigurationFileName = $"{AssemblyName}.json";
		CurrentCulture = CultureInfo.CurrentCulture;
		MachineName = Environment.MachineName;
		UserName = Environment.UserName;
	}
}
