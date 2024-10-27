using Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using Application.Interfaces.Infrastructure.Services;
using Application.Services.Documents;

using AutoMapper;

using Moq;

namespace ApplicationTests.Services.Documents;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class DocumentServiceTests : ApplicationTestBase
{
	private readonly IMapper _mapper = GetService<IMapper>();
	private Mock<ILoggerService<DocumentService>> _loggerServiceMock = default!;
	private Mock<IRepositoryService> _repositoryServiceMock = default!;

	private DocumentService CreateMockedInstance(IDocumentRepository? documentRepository = null, IDocumentExtensionRepository? extensionRepository = null, IDocumentDataRepository? dataRepository = null)
	{
		_loggerServiceMock = new();
		_repositoryServiceMock = new();

		if (documentRepository is not null)
			_repositoryServiceMock.Setup(x => x.DocumentRepository)
				.Returns(documentRepository);

		if (extensionRepository is not null)
			_repositoryServiceMock.Setup(x => x.DocumentExtensionRepository)
				.Returns(extensionRepository);

		if (dataRepository is not null)
			_repositoryServiceMock.Setup(x => x.DocumentDataRepository)
				.Returns(dataRepository);

		return new(_loggerServiceMock.Object, _repositoryServiceMock.Object, _mapper);
	}
}
