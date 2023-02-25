using Debug.ConsoleApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.ConsoleApp.Services;

internal sealed class ExampleScopedService : IExampleScopedService
{
	Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
}
