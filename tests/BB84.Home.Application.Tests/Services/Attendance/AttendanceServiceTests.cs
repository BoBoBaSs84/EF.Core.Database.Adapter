using AutoMapper;

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
	private readonly AttendanceService _sut;
	private readonly IMapper _mapper = GetService<IMapper>();
	private readonly Mock<ILoggerService<AttendanceService>> _loggerServiceMock = new();
	private readonly Mock<IRepositoryService> _repositoryServiceMock = new();
	private readonly Mock<ICurrentUserService> _currentUserService = new();

	public AttendanceServiceTests()
		=> _sut = new(_loggerServiceMock.Object, _currentUserService.Object, _repositoryServiceMock.Object, _mapper);
}