using BB84.Extensions.Serialization;
using BB84.Home.Domain.Entities.Identity;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using XmlConstants = BB84.Home.Domain.Common.DomainConstants.Xml;

namespace Infrastructure.Persistence.Converters;

/// <summary>
/// The preferences value converter.
/// </summary>
internal sealed class PreferencesConverter : ValueConverter<PreferencesModel, string>
{
	/// <summary>
	/// Initilizes an instance of the <see cref="PreferencesConverter"/> class.
	/// </summary>
	public PreferencesConverter() : base(x => x.ToXml(XmlConstants.GetNamespaces(), null), x => x.FromXml<PreferencesModel>(null))
	{ }
}
