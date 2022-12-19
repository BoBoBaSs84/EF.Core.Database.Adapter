using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.Extensions;

internal static class ModelBuilderExtension
{
	/// <summary>
	/// Wraps the "ApplyConfigurationsFromAssembly" method.
	/// </summary>
	/// <param name="modelBuilder">The <see cref="ModelBuilder"/> itself.</param>
	public static void ApplyConfigurationsForContextEntities(this ModelBuilder modelBuilder)
	{
		HashSet<Type> types = modelBuilder.Model.GetEntityTypes().Select(t => t.ClrType).ToHashSet();

		_ = modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(ModelBuilderExtension).Assembly,
				t => t.GetInterfaces().Any(i => i.IsGenericType
					&& i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
					&& types.Contains(i.GenericTypeArguments[0]))
			);
	}

	// TODO: find a good solution ...
	public static void ApplyNonClusteredIndexOnGloballyUniqueIdentifier(this ModelBuilder modelBuilder)
	{
		HashSet<Type> types = modelBuilder.Model.GetEntityTypes().Select(t => t.ClrType).ToHashSet();

	}
}
