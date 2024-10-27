using Application.Contracts.Responses.Documents;

using AutoMapper;

using Domain.Models.Documents;

namespace Application.Profiles.Responses;

/// <summary>
/// The attendance response profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class DocumentsResponseProfile : Profile
{
	public DocumentsResponseProfile()
	{
		CreateMap<Document, DocumentResponse>()
			.ForMember(dest => dest.MD5Hash, opt => opt.MapFrom(src => HasData(src) ? src.Data.MD5Hash : default))
			.ForMember(dest => dest.Length, opt => opt.MapFrom(src => HasData(src) ? src.Data.Length : default))
			.ForMember(dest => dest.Content, opt => opt.MapFrom(src => HasData(src) ? src.Data.Content : default))
			.ForMember(dest => dest.ExtenionName, opt => opt.MapFrom(src => HasExtension(src) ? src.Extension.Name : default))
			.ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => HasExtension(src) ? src.Extension.MimeType : default));
	}

	private static bool HasData(Document document)
		=> document.Data is not null;

	private static bool HasExtension(Document document)
		=> document.Extension is not null;
}
