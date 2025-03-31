namespace BB84.Home.Application.Features.Responses;

/// <summary>
/// The paged list interface.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IList{T}"/> interface.
/// </remarks>
/// <typeparam name="T"></typeparam>
public interface IPagedList<T> : IList<T> where T : class
{
	/// <summary>
	/// The meta data for the paged list.
	/// </summary>
	public MetaData MetaData { get; }
}
