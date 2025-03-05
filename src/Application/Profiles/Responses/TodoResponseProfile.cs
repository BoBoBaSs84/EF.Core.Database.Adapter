using System.Drawing;

using AutoMapper;

using BB84.Extensions;
using BB84.Home.Application.Contracts.Responses.Todo;
using BB84.Home.Domain.Entities.Todo;

namespace BB84.Home.Application.Profiles.Responses;

/// <summary>
/// The todo response profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class TodoResponseProfile : Profile
{
	public TodoResponseProfile()
	{
		CreateMap<ItemEntity, ItemResponse>();
		CreateMap<ListEntity, ListResponse>()
			.ForMember(t => t.Color, o => o.MapFrom(s => MapColor(s.Color)));
	}

	private static string? MapColor(Color? color)
		=> color?.ToRGBHexString();
}
