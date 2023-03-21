namespace Application.Features.Requests.Base;

public abstract class EnumeratorModelParameters : RequestParameters
{
	public string? Name { get; set; }

	public string? Description { get; set; }

	public bool? IsActive { get; set; }
}
