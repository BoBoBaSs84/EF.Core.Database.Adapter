using AutoMapper;

using BB84.Home.Application.Contracts.Responses.Documents;
using BB84.Home.Domain.Entities.Documents;

namespace BB84.Home.Application.Profiles.Responses;

/// <summary>
/// The attendance response profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class DocumentsResponseProfile : Profile
{
	public DocumentsResponseProfile()
	{
		CreateMap<DocumentEntity, DocumentResponse>()
			.ForMember(dest => dest.MD5Hash, opt => opt.MapFrom(src => HasData(src) ? src.Data.MD5Hash : default))
			.ForMember(dest => dest.Length, opt => opt.MapFrom(src => HasData(src) ? src.Data.Length : default))
			.ForMember(dest => dest.Content, opt => opt.MapFrom(src => HasData(src) ? src.Data.Content : default))
			.ForMember(dest => dest.ExtensionName, opt => opt.MapFrom(src => HasExtension(src) ? src.Extension.Name : default))
			.ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => HasExtension(src) ? src.Extension.MimeType : default));
	}

	private static bool HasData(DocumentEntity document)
		=> document.Data is not null;

	private static bool HasExtension(DocumentEntity document)
		=> document.Extension is not null;
}
