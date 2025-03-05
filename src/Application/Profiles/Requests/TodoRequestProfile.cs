using System.Drawing;

using AutoMapper;

using BB84.Extensions;
using BB84.Home.Application.Contracts.Requests.Todo;
using BB84.Home.Domain.Entities.Todo;

namespace BB84.Home.Application.Profiles.Requests;

/// <summary>
/// The todo request profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class TodoRequestProfile : Profile
{
	public TodoRequestProfile()
	{
		CreateMap<ListCreateRequest, ListEntity>()
			.ForMember(t => t.Color, o => o.MapFrom(s => MapColor(s.Color)));
		CreateMap<ListUpdateRequest, ListEntity>()
			.ForMember(t => t.Color, o => o.MapFrom(s => MapColor(s.Color)));
		CreateMap<ItemCreateRequest, ItemEntity>();
		CreateMap<ItemUpdateRequest, ItemEntity>();
	}

	private static Color? MapColor(string? color)
		=> color?.FromRGBHexString();
}
