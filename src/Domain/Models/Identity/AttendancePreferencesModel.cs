using System.Xml.Serialization;

using Domain.Enumerators;

namespace Domain.Models.Identity;

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
	/// <param name="vacationDays">The vacation days per year to set.</param>
	public AttendancePreferencesModel(WorkDayTypes workDays, float workHours, int vacationDays)
	{
		WorkDays = workDays;
		WorkHours = workHours;
		VacationDays = vacationDays;
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

	/// <summary>
	/// The vacation days per year.
	/// </summary>
	[XmlElement("VacationDays")]
	public int VacationDays { get; set; }
}
