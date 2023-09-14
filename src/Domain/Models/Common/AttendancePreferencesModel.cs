using System.Xml.Serialization;

using Domain.Enumerators;

namespace Domain.Models.Common;

/// <summary>
/// The attendance preferences model class.
/// </summary>
[XmlRoot("AttendancePreferences")]
public sealed class AttendancePreferencesModel
{
	/// <summary>
	/// Initilizes an instance of the attendance preferences model class.
	/// </summary>
	public AttendancePreferencesModel()
	{ }

	/// <summary>
	/// Initilizes an instance of the attendance preferences model class.
	/// </summary>
	/// <param name="workDays">The work days per week to set.</param>
	/// <param name="workHours">The work hours per week to set.</param>
	public AttendancePreferencesModel(WorkDayTypes workDays, float workHours)
	{
		WorkDays = workDays;
		WorkHours = workHours;
	}

	/// <summary>
	/// The work days per week property.
	/// </summary>
	[XmlElement("WorkDays")]
	public WorkDayTypes WorkDays { get; set; }

	/// <summary>
	/// The work hours per week property.
	/// </summary>
	[XmlElement("WorkHours")]
	public float WorkHours { get; set; }
}
