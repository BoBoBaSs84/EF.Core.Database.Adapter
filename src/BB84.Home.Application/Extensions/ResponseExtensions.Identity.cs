using BB84.Home.Application.Contracts.Responses.Identity;
using BB84.Home.Domain.Entities.Identity;

namespace BB84.Home.Application.Extensions;

internal static partial class ResponseExtensions
{
	/// <summary>
	/// Converts a <see cref="UserEntity"/> to a <see cref="UserResponse"/>.
	/// </summary>
	/// <param name="entity">The <see cref="UserEntity"/> to convert.</param>
	/// <returns>The converted <see cref="UserResponse"/>.</returns>
	public static UserResponse ToResponse(this UserEntity entity)
	{
		PreferencesResponse? preferences = null;
		if (entity.Preferences is not null && entity.Preferences.AttendancePreferences is not null)
		{
			preferences = new PreferencesResponse
			{
				AttendancePreferences = new AttendancePreferencesResponse
				{
					WorkDays = entity.Preferences.AttendancePreferences.WorkDays,
					WorkHours = entity.Preferences.AttendancePreferences.WorkHours,
					VacationDays = entity.Preferences.AttendancePreferences.VacationDays
				}
			};
		}

		UserResponse response = new()
		{
			Id = entity.Id,
			FirstName = entity.FirstName,
			MiddleName = entity.MiddleName,
			LastName = entity.LastName,
			DateOfBirth = entity.DateOfBirth,
			UserName = entity.UserName!,
			Email = entity.Email!,
			PhoneNumber = entity.PhoneNumber,
			Picture = entity.Picture,
			Preferences = preferences
		};

		return response;
	}
}
