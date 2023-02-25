﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.ConsoleApp.Services.Interface;

public interface IExampleScopedService : IReportServiceLifetime
{
	ServiceLifetime IReportServiceLifetime.Lifetime => ServiceLifetime.Scoped;
}
