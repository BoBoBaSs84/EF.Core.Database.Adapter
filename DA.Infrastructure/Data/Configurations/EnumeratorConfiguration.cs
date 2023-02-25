using DA.Domain.Extensions;
using DA.Domain.Models.MasterData;
using DA.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static DA.Domain.Constants.Sql.Schema;
using ECT = DA.Domain.Enumerators.CardType;
using EDT = DA.Domain.Enumerators.DayType;

namespace DA.Infrastructure.Data.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class EnumeratorConfiguration
{
	/// <inheritdoc/>
	internal sealed class CardTypeConfiguration : IEntityTypeConfiguration<CardType>
	{
		public void Configure(EntityTypeBuilder<CardType> builder)
		{
			builder.ToSytemVersionedTable(nameof(CardType), ENUMERATOR);

			builder.HasKey(e => e.Id)
				.IsClustered(true);

			builder.HasMany(e => e.Cards)
				.WithOne(e => e.CardType)
				.HasForeignKey(e => e.CardTypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasData(GetEnDayTypes());
		}

		private static IEnumerable<CardType> GetEnDayTypes()
		{
			List<ECT> enumList = ECT.CREDIT.GetListFromEnum();
			IList<CardType> listToReturn = new List<CardType>();

			foreach (ECT dayType in enumList)
				listToReturn.Add(new CardType()
				{
					Id = (int)dayType,
					Name = dayType.GetName(),
					Description = dayType.GetDescription(),
					IsActive = true
				});

			return listToReturn;
		}
	}

	/// <inheritdoc/>
	internal sealed class DayTypeConfiguration : IEntityTypeConfiguration<DayType>
	{
		/// <inheritdoc/>
		public void Configure(EntityTypeBuilder<DayType> builder)
		{
			builder.ToSytemVersionedTable(nameof(DayType), ENUMERATOR);

			builder.HasKey(e => e.Id)
				.IsClustered(false);

			builder.HasMany(e => e.CalendarDays)
				.WithOne(e => e.DayType)
				.HasForeignKey(e => e.DayTypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasMany(e => e.Attendances)
				.WithOne(e => e.DayType)
				.HasForeignKey(e => e.DayTypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasData(GetEnDayTypes());
		}

		private static IEnumerable<DayType> GetEnDayTypes()
		{
			List<EDT> enumList = EDT.ABSENCE.GetListFromEnum();
			IList<DayType> listToReturn = new List<DayType>();

			foreach (EDT dayType in enumList)
				listToReturn.Add(new DayType()
				{
					Id = (int)dayType,
					Name = dayType.GetName(),
					Description = dayType.GetDescription(),
					IsActive = true
				});

			return listToReturn;
		}
	}
}
