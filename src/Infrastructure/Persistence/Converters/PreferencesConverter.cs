using Domain.Extensions;
using Domain.Models.Common;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Converters;

/// <summary>
/// The preferences value converter.
/// </summary>
internal sealed class PreferencesConverter : ValueConverter<PreferencesModel, string>
{
	/// <summary>
	/// Initilizes an instance of the preferences value converter.
	/// </summary>
	public PreferencesConverter() : base(x => x.ToXmlString(null, null), x => new PreferencesModel().FromXmlString(x, null))
	{ }
}
