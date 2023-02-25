using DA.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DA.Infrastructure.Extensions;

internal static class ModelBuilderExtension
{
	/// <summary>
	/// Wraps the "ApplyConfigurationsFromAssembly" method.
	/// </summary>
	/// <param name="modelBuilder">The <see cref="ModelBuilder"/> itself.</param>
	[SuppressMessage("Style", "IDE0058", Justification = "Not needed here.")]
	public static void ApplyConfigurationsForContextEntities(this ModelBuilder modelBuilder)
	{
		HashSet<Type> types = modelBuilder.Model.GetEntityTypes().Select(t => t.ClrType).ToHashSet();

		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(IInfrastructureAssemblyMarker).Assembly,
			t => t.GetInterfaces().Any(i => i.IsGenericType
			&& i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
			&& types.Contains(i.GenericTypeArguments[0]))
			);
	}
}
