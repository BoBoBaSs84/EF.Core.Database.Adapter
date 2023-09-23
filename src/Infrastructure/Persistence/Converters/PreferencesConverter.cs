using Domain.Extensions;
using Domain.Models.Identity;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using XmlConstants = Domain.Constants.DomainConstants.Xml;

namespace Infrastructure.Persistence.Converters;

/// <summary>
/// The preferences value converter.
/// </summary>
internal sealed class PreferencesConverter : ValueConverter<PreferencesModel, string>
{
	/// <summary>
	/// Initilizes an instance of the preferences value converter.
	/// </summary>
	public PreferencesConverter() : base(x => x.ToXmlString(XmlConstants.GetNamespaces(), null), x => new PreferencesModel().FromXmlString(x, null))
	{ }
}
