using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Application.Services.Attendance;
using BB84.Home.Application.Tests;

using Moq;

namespace ApplicationTests.Services.Attendance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AttendanceServiceTests : ApplicationTestBase
{
	private readonly CancellationToken _cancellationToken;
	private readonly AttendanceService _sut;
	private readonly Mock<ILoggerService<AttendanceService>> _loggerServiceMock = new();
	private readonly Mock<IRepositoryService> _repositoryServiceMock = new();
	private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();

	public AttendanceServiceTests()
	{
		_cancellationToken = CancellationToken.None;
		_sut = new(_loggerServiceMock.Object, _currentUserServiceMock.Object, _repositoryServiceMock.Object);
	}
}