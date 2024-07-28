using System.Drawing;

using Application.Contracts.Responses.Todo;

using AutoMapper;

using BB84.Extensions;

using Domain.Models.Todo;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class ResponseProfiles
{
	/// <inheritdoc/>
	internal sealed class TodoResponseProfile : Profile
	{
		public TodoResponseProfile()
		{
			CreateMap<Item, ItemResponse>();
			CreateMap<List, ListResponse>()
				.ForMember(t => t.Color, o => o.MapFrom(s => MapColor(s.Color)));
		}

		private static string? MapColor(Color? color)
			=> color?.ToRGBHexString();
	}
}
