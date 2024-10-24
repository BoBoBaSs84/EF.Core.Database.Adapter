using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Requests.Documents;

public sealed class DocumentCreateRequest
{
	public required byte[] MD5Hash { get; init; }
}
