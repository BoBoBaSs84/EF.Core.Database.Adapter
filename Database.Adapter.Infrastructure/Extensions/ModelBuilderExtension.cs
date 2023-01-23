using Database.Adapter.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure.Extensions;

internal static class ModelBuilderExtension
{
	/// <summary>
	/// Wraps the "ApplyConfigurationsFromAssembly" method.
	/// </summary>
	/// <param name="modelBuilder">The <see cref="ModelBuilder"/> itself.</param>
	/// <returns>The <see cref="ModelBuilder"/> itself.</returns>
	public static ModelBuilder ApplyConfigurationsForContextEntities(this ModelBuilder modelBuilder)
	{
		HashSet<Type> types = modelBuilder.Model.GetEntityTypes().Select(t => t.ClrType).ToHashSet();

		return modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(IAssemblyMarker).Assembly,
				t => t.GetInterfaces().Any(i => i.IsGenericType
					&& i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
					&& types.Contains(i.GenericTypeArguments[0]))
			);
	}
}
