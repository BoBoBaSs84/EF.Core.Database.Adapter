using BB84.Home.Application.Contracts.Requests.Identity;
using BB84.Home.Domain.Entities.Identity;

namespace BB84.Home.Application.Extensions;

internal static partial class RequestExtensions
{
	/// <summary>
	/// Converts a <see cref="UserCreateRequest"/> to a <see cref="UserEntity"/>.
	/// </summary>
	/// <param name="request">The <see cref="UserCreateRequest"/> to convert.</param>
	/// <returns>The converted <see cref="UserEntity"/>.</returns>
	public static UserEntity ToEntity(this UserCreateRequest request)
	{
		UserEntity entity = new()
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			Email = request.Email,
			UserName = request.UserName
		};

		return entity;
	}

	/// <summary>
	/// Converts a <see cref="UserUpdateRequest"/> to a <see cref="UserEntity"/>.
	/// </summary>
	/// <param name="request">The <see cref="UserUpdateRequest"/> to convert.</param>
	/// <param name="entity">The entity to update with the values from the request.</param>
	/// <returns>The updated <see cref="UserEntity"/>.</returns>
	public static UserEntity ToEntity(this UserUpdateRequest request, UserEntity entity)
	{
		entity.FirstName = request.FirstName;
		entity.LastName = request.LastName;
		entity.MiddleName = request.MiddleName;
		entity.DateOfBirth = request.DateOfBirth;
		entity.PhoneNumber = request.PhoneNumber;
		entity.Picture = request.Picture;

		if (request.Preferences is not null)
		{
			var attendancePreferences = request.Preferences.AttendancePreferences;
			if (attendancePreferences is not null)
			{
				entity.Preferences = new PreferencesModel(
					new AttendancePreferencesModel(attendancePreferences.WorkDays, attendancePreferences.WorkHours, attendancePreferences.VacationDays));
			}
		}

		return entity;
	}
}
