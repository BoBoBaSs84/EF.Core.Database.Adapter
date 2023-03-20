using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Common;

public abstract class BaseEnumeratorModel : BaseResponseModel
{
	public int Id { get; set; } = default!;

	public string Name { get; set; } = default!;

	public string? Description { get; set; } = default!;

	public bool IsActive { get; set; } = default!;
}
