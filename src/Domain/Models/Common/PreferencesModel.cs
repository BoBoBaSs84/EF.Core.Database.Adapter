using System.Xml.Serialization;

namespace Domain.Models.Common;

/// <summary>
/// The preferences model class.
/// </summary>
[XmlRoot("Preferences")]
public sealed class PreferencesModel
{
	/// <summary>
	/// Initilizes an instance of the preferences model class.
	/// </summary>
	public PreferencesModel()
		=> AttendancePreferences = new();

	/// <summary>
	/// Initilizes an instance of the preferences model class.
	/// </summary>
	/// <param name="attendancePreferences">The attendance preferences to set.</param>
	public PreferencesModel(AttendancePreferencesModel attendancePreferences)
		=> AttendancePreferences = attendancePreferences;

	/// <summary>
	/// The attendance preferences property.
	/// </summary>
	[XmlElement("AttendancePreferences")]
	public AttendancePreferencesModel AttendancePreferences { get; set; }
}
