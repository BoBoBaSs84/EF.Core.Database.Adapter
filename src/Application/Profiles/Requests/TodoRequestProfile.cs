using System.Drawing;

using Application.Contracts.Requests.Todo;

using AutoMapper;

using BB84.Extensions;

using Domain.Models.Todo;

namespace Application.Profiles.Requests;

/// <summary>
/// The todo request profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class TodoRequestProfile : Profile
{
	public TodoRequestProfile()
	{
		CreateMap<ListCreateRequest, List>()
			.ForMember(t => t.Color, o => o.MapFrom(s => MapColor(s.Color)));
		CreateMap<ListUpdateRequest, List>()
			.ForMember(t => t.Color, o => o.MapFrom(s => MapColor(s.Color)));
		CreateMap<ItemCreateRequest, Item>();
		CreateMap<ItemUpdateRequest, Item>();
	}

	private static Color? MapColor(string? color)
		=> color?.FromRGBHexString();
}
