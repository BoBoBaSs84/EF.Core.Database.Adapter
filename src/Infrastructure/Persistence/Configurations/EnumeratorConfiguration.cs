using Domain.Entities.Enumerator;
using Domain.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECT = Domain.Enumerators.CardTypes;
using EDT = Domain.Enumerators.DayTypes;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class EnumeratorConfiguration
{
	/// <inheritdoc/>
	internal sealed class CardTypeConfiguration : EnumeratorTypeBaseConfiguration<CardType>
	{
		public override void Configure(EntityTypeBuilder<CardType> builder)
		{
			builder.ToSytemVersionedTable(SqlSchema.ENUMERATOR);

			builder.HasMany(e => e.Cards)
				.WithOne(e => e.CardType)
				.HasForeignKey(e => e.CardTypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			builder.HasData(GetCardTypeData());

			base.Configure(builder);
		}

		private static IEnumerable<CardType> GetCardTypeData()
		{
			IList<ECT> enumList = ECT.CREDIT.GetListFromEnum();
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
	internal sealed class DayTypeConfiguration : EnumeratorTypeBaseConfiguration<DayType>
	{
		/// <inheritdoc/>
		public override void Configure(EntityTypeBuilder<DayType> builder)
		{
			builder.ToSytemVersionedTable(SqlSchema.ENUMERATOR);

			builder.HasMany(e => e.CalendarDays)
				.WithOne(e => e.DayType)
				.HasForeignKey(e => e.DayTypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			builder.HasMany(e => e.Attendances)
				.WithOne(e => e.DayType)
				.HasForeignKey(e => e.DayTypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			builder.HasData(GetDayTypeData());

			base.Configure(builder);
		}

		private static IEnumerable<DayType> GetDayTypeData()
		{
			IList<EDT> enumList = EDT.ABSENCE.GetListFromEnum();
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
