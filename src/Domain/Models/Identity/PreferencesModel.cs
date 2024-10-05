using System.Xml.Serialization;

using Domain.Enumerators.Attendance;

using XmlDataType = Domain.Common.DomainConstants.Xml.DataType;
using XmlElements = Domain.Common.DomainConstants.Xml.Elements;
using XmlNameSpaces = Domain.Common.DomainConstants.Xml.NameSpaces;

namespace Domain.Models.Identity;

/// <summary>
/// The preferences model class.
/// </summary>
[XmlRoot(XmlElements.Preferences, Namespace = XmlNameSpaces.Preferences)]
public sealed class PreferencesModel
{
	/// <summary>
	/// Initilizes an instance of the preferences model class.
	/// </summary>
	public PreferencesModel()
	{ }

	/// <summary>
	/// Initilizes an instance of the preferences model class.
	/// </summary>
	/// <param name="attendancePreferences">The attendance preferences to set.</param>
	public PreferencesModel(AttendancePreferencesModel attendancePreferences)
		=> AttendancePreferences = attendancePreferences;

	/// <summary>
	/// The attendance preferences property.
	/// </summary>
	[XmlElement(XmlElements.Attendance)]
	public AttendancePreferencesModel? AttendancePreferences { get; set; }
}

/// <summary>
/// The attendance preferences model class.
/// </summary>
[XmlRoot(XmlElements.Attendance, Namespace = XmlNameSpaces.Preferences)]
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
	[XmlIgnore]
	public WorkDayTypes WorkDays { get; set; }

	/// <summary>
	/// The work days backing property.
	/// </summary>
	/// <remarks>
	/// To save the <see cref="WorkDays"/> as an int interpretation.
	/// </remarks>
	[XmlElement(XmlElements.WorkDays, DataType = XmlDataType.INT)]
	public int WorkDayTypes
	{
		get => (int)WorkDays;
		set => WorkDays = (WorkDayTypes)value;
	}

	/// <summary>
	/// The work hours per week property.
	/// </summary>
	[XmlElement(XmlElements.WorkHours, DataType = XmlDataType.FLOAT)]
	public float WorkHours { get; set; }

	/// <summary>
	/// The vacation days per year.
	/// </summary>
	[XmlElement(XmlElements.VacationDays, DataType = XmlDataType.INT)]
	public int VacationDays { get; set; }
}
