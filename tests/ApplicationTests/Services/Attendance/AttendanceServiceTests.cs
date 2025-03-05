using AutoMapper;

using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Services.Attendance;
using BB84.Home.Application.Tests;

using Moq;

namespace ApplicationTests.Services.Attendance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AttendanceServiceTests : ApplicationTestBase
{
	private readonly IMapper _mapper = GetService<IMapper>();
	private Mock<ILoggerService<AttendanceService>> _loggerServiceMock = default!;
	private Mock<IRepositoryService> _repositoryServiceMock = default!;

	private AttendanceService CreateMockedInstance(IAttendanceRepository? attendanceRepository = null)
	{
		_loggerServiceMock = new();
		_repositoryServiceMock = new();

		if (attendanceRepository is not null)
			_repositoryServiceMock.Setup(x => x.AttendanceRepository).Returns(attendanceRepository);

		return new(_loggerServiceMock.Object, _repositoryServiceMock.Object, _mapper);
	}
}