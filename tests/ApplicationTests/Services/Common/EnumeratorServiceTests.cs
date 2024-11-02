using Application.Interfaces.Infrastructure.Services;
using Application.Services.Common;

using AutoMapper;

using Moq;

namespace ApplicationTests.Services.Common;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class EnumeratorServiceTests : ApplicationTestBase
{
	private readonly IMapper _mapper = GetService<IMapper>();
	private Mock<ILoggerService<EnumeratorService>> _loggerServiceMock = default!;

	private EnumeratorService CreateMockedInstance(IMapper mapper)
	{
		_loggerServiceMock = new();

		return new(_loggerServiceMock.Object, mapper);
	}
}