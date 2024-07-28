using System.Drawing;

using Application.Contracts.Requests.Todo;

using AutoMapper;

using BB84.Extensions;

using Domain.Models.Todo;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class RequestProfiles
{
	/// <inheritdoc/>
	internal sealed class TodoRequestProfile : Profile
	{
		public TodoRequestProfile()
		{
			CreateMap<ListCreateRequest, List>()
				.ForMember(t => t.Color, o => o.MapFrom(s => MapColor(s.Color)));
			CreateMap<ListUpdateRequest, List>();
			CreateMap<ItemCreateRequest, Item>();
			CreateMap<ItemUpdateRequest, Item>();
		}

		private static Color? MapColor(string? color)
			=> color?.FromRGBHexString();
	}
}
