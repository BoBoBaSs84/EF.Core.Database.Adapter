﻿using AutoMapper;

using BB84.Extensions;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Services.Documents;
using BB84.Home.Application.Tests;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Documents;
using BB84.Home.Domain.Enumerators.Documents;

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

	private static DocumentEntity CreateDocument(Guid? id = null)
	{
		Guid dataId = Guid.NewGuid(),
			extensionId = Guid.NewGuid();

		string radomContent = RandomHelper.GetString(100);

		DocumentEntity document = new()
		{
			CreationTime = RandomHelper.GetDateTime(),
			Data = new()
			{
				Id = dataId,
				MD5Hash = radomContent.GetBytes().GetMD5(),
				Content = radomContent.GetBytes(),
				Length = radomContent.GetBytes().Length
			},
			DataId = dataId,
			Directory = RandomHelper.GetString(50),
			Extension = new()
			{
				Id = extensionId,
				Name = RandomHelper.GetString(3),
				MimeType = RandomHelper.GetString(10)
			},
			ExtensionId = extensionId,
			Flags = DocumentTypes.Invoice,
			Id = id ?? Guid.NewGuid(),
			LastAccessTime = RandomHelper.GetDateTime(),
			LastWriteTime = RandomHelper.GetDateTime(),
			Name = RandomHelper.GetString(18)
		};

		return document;
	}
}
