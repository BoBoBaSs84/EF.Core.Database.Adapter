using Domain.Entities.Enumerator;
using Domain.Extensions;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECT = Domain.Enumerators.CardTypes;
using EDT = Domain.Enumerators.DayTypes;
using Schema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class EnumeratorConfiguration
{
	/// <inheritdoc/>
	internal sealed class CardTypeConfiguration : IEntityTypeConfiguration<CardType>
	{
		public void Configure(EntityTypeBuilder<CardType> builder)
		{
			builder.ToSytemVersionedTable(nameof(CardType), Schema.ENUMERATOR);

			builder.HasKey(e => e.Id)
				.IsClustered(true);

			builder.HasIndex(e => e.Name)
				.IsClustered(false)
				.IsUnique(true);

			builder.HasMany(e => e.Cards)
				.WithOne(e => e.CardType)
				.HasForeignKey(e => e.CardTypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasData(GetCardTypes());
		}

		private static IEnumerable<CardType> GetCardTypes()
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
			builder.ToSytemVersionedTable(nameof(DayType), Schema.ENUMERATOR);

			builder.HasKey(e => e.Id)
				.IsClustered(true);

			builder.HasIndex(e => e.Name)
				.IsClustered(false)
				.IsUnique(true);

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

			builder.HasData(GetDayTypes());
		}

		private static IEnumerable<DayType> GetDayTypes()
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
