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
			.ForMember(dest => dest.MD5Hash, opt => opt.MapFrom(src => src.DocumentDatas.FirstOrDefault().Data.MD5Hash))
			.ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.DocumentDatas.FirstOrDefault().Data.Length))
			.ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.DocumentDatas.FirstOrDefault().Data.Content))
			.ForMember(dest => dest.ExtenionName, opt => opt.MapFrom(src => src.Extension.Name))
			.ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => src.Extension.MimeType));
	}
}
