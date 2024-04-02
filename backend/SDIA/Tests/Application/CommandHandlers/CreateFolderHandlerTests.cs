using Application.Commands.Folder;
using Application.DTOs;
using Application.Entities.Response;
using Application.Handlers.Folder;
using Application.Interfaces.Services;
using Application.SignalR;
using Domain.Constants.Enums;
using Infrastructure.DbContext;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SDIA.Configurations;

namespace Tests.Application.CommandHandlers;

public class CreateFolderHandlerTests
{
    private ServiceProvider _serviceProvider;
    private Mock<IDocumentService> _documentServiceMock = new();
    private Mock<IImageService> _imageServiceMock = new();
    private Mock<ICreateFolderNotification> _createFolderNotification = new();

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddDbContext<SdiaDbContext>(options => { options.UseInMemoryDatabase("Sdia"); });
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(MapsterConfiguration).Assembly);

        _serviceProvider = services.BuildServiceProvider();
    }

    [TestCase("Car never registered", FolderType.CarNeverRegistered)]
    [TestCase("Car from another country", FolderType.CarFromAnotherCountry)]
    [TestCase("Car already registered in country", FolderType.AlreadyRegisteredVehicleInCountry)]
    public void CreateFolderHandler_ValidEntity_ShouldCreateFolder(string folderName, FolderType folderType)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SdiaDbContext>();
        var uploadedUrl = "url";
        var createFolderCommand = new CreateFolderCommand()
        {
            Documents = new List<CreateDocumentDto>()
            {
                new CreateDocumentDto()
                {
                    DocumentType = DocumentType.IdentityCard,
                    File = new FormFile(new MemoryStream(new byte[1]), 0, 0, "", "filename"),
                }
            },
            FolderType = folderType,
            Name = folderName
        };

        _documentServiceMock.Setup(x => x.AnalyzeDocumentAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(DocumentType.IdentityCard);

        _imageServiceMock.Setup(x =>
                x.UploadImageAsync(It.IsAny<string>(), It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(uploadedUrl);

        _createFolderNotification
            .Setup(x => x.SendNewStatus(It.IsAny<CreateFolderNotificationResponse>(), It.IsAny<Guid>(),
                CancellationToken.None))
            .Returns(Task.CompletedTask);

        var createFolderHandler = new CreateFolderHandler(dbContext, _imageServiceMock.Object,
            _documentServiceMock.Object, _createFolderNotification.Object);

        var result = createFolderHandler.Handle(createFolderCommand, CancellationToken.None).Result;

        Assert.NotNull(result);
        Assert.That(result.Name, Is.EqualTo(folderName));
        Assert.That(result.Type, Is.EqualTo(folderType));

        Assert.That(result.Documents.Count, Is.EqualTo(1));
        Assert.That(result.Documents[0].DocumentType, Is.EqualTo(DocumentType.IdentityCard));
        Assert.That(result.Documents[0].StorageUrl, Is.EqualTo(uploadedUrl));
    }

    [TearDown]
    public void Cleanup()
    {
        var dbContext = _serviceProvider.GetService<SdiaDbContext>();
        dbContext.Database.EnsureDeleted();
    }
}