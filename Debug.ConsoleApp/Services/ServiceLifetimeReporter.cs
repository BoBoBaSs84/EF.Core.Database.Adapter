using Debug.ConsoleApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.ConsoleApp.Services;

internal sealed class ServiceLifetimeReporter
{
	private readonly IExampleTransientService _transientService;
	private readonly IExampleScopedService _scopedService;
	private readonly IExampleSingletonService _singletonService;

	public ServiceLifetimeReporter(IExampleTransientService transientService,IExampleScopedService scopedService,IExampleSingletonService singletonService)
	{
		_transientService = transientService;
		_scopedService = scopedService;
		_singletonService = singletonService;
	}

	public void ReportServiceLifetimeDetails(string lifetimeDetails)
	{
		Console.WriteLine(lifetimeDetails);

		LogService(_transientService, "Always different");
		LogService(_scopedService, "Changes only with lifetime");
		LogService(_singletonService, "Always the same");
	}

	private static void LogService<T>(T service, string message)
			where T : IReportServiceLifetime =>
			Console.WriteLine(
					$"    {typeof(T).Name}: {service.Id} ({message})");
}
