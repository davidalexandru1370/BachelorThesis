using System.Security.Claims;
using Application.DTOs;
using Application.Query.Folder;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.DbContext;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SDIA.Configurations;
using SDIA.Controllers;
using SDIA.Entities.Folder.Responses;

namespace Tests.Application.QueryHandlers
{
    class GetFoldersByUserIdQueryTests
    {
        private Mock<IMediator> _mediator = new Mock<IMediator>();

        #region USER

        private Guid _userId = Guid.Parse("2cc9f976-60af-4c26-97c9-d935cd37a100");

        #endregion

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<SdiaDbContext>(options => { options.UseInMemoryDatabase("Sdia"); });

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MapsterConfiguration).Assembly);
        }

        [TestCase]
        public async Task GetFoldersByUserIdQuery_ValidUserId_ShouldReturnEmptyList()
        {
            var contextMock = new Mock<HttpContext>();
            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, _userId.ToString())
            }));

            contextMock.Setup(x => x.User).Returns(claims);
            var context = new ControllerContext(new ActionContext(contextMock.Object,
                new RouteData(),
                new ControllerActionDescriptor()));

            _mediator.Setup(x => x.Send(It.IsAny<GetFoldersByUserIdQuery>, default))
                .ReturnsAsync(new List<FolderDto>());

            FolderController folderController = new FolderController(_mediator.Object)
            {
                ControllerContext = context
            };

            var result = await folderController.GetFolders();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [TestCase("")]
        public async Task GetFoldersByUserIdQuery_InvalidUserId_ShouldThrowNotAuthenticatedException(
            string? invalidUserId)
        {
            var contextMock = new Mock<HttpContext>();
            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, invalidUserId)
            }));

            contextMock.Setup(x => x.User).Returns(claims);
            var context = new ControllerContext(new ActionContext(contextMock.Object,
                new RouteData(),
                new ControllerActionDescriptor()));

            FolderController folderController = new FolderController(_mediator.Object)
            {
                ControllerContext = context
            };

            Assert.ThrowsAsync<NotAuthenticatedException>(() => folderController.GetFolders());
        }

        [TestCase]
        public async Task GetFoldersByUserIQuery_ValidUserId_ShouldReturnListOfFolders()
        {
            var contextMock = new Mock<HttpContext>();
            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, _userId.ToString())
            }));

            var folderId = Guid.NewGuid();

            contextMock.Setup(x => x.User).Returns(claims);
            var context = new ControllerContext(new ActionContext(contextMock.Object,
                new RouteData(),
                new ControllerActionDescriptor()));

            List<FolderDto> foldersQueryResult = new List<FolderDto>();

            int numberOfFolders = 5;

            for (int index = 0; index < numberOfFolders; index++)
            {
                var folderDto = new FolderDto
                {
                    Id = folderId,
                    Name = $"Folder-{index}",
                    User = new UserDto { Id = _userId },
                    Documents = new List<DocumentDto>
                    {
                        new DocumentDto()
                        {
                            DocumentType = DocumentType.IdentityCard,
                            StorageUrl = $"url-document-{index}-{1}"
                        },
                        new DocumentDto()
                        {
                            DocumentType = DocumentType.NotFound,
                            StorageUrl = $"url-document-{index}-{2}"
                        },
                        new DocumentDto()
                        {
                            DocumentType = DocumentType.OwnershipContract,
                            StorageUrl = $"url-document-{index}-{3}"
                        }
                    },
                    Type = FolderType.CarNeverRegistered,
                    Errors = new List<string>(),
                    IsCorrect = false
                };

                foldersQueryResult.Add(folderDto);
            }

            _mediator.Setup(x => x.Send(It.IsAny<GetFoldersByUserIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(foldersQueryResult);

            FolderController folderController = new FolderController(_mediator.Object)
            {
                ControllerContext = context
            };

            var result = await folderController.GetFolders();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.That(result.Result as OkObjectResult, Is.Not.Null);
            Assert.That((result.Result as OkObjectResult).Value, Is.InstanceOf<List<FolderInfoResponse>>());

            var foldersResponse = (result.Result as OkObjectResult).Value as List<FolderInfoResponse>;

            Assert.That(foldersResponse.Count, Is.EqualTo(5));
            for (int index = 0; index < numberOfFolders; index++)
            {
                Assert.That(foldersResponse[index].Name, Is.EqualTo($"Folder-{index}"));
                Assert.That(foldersResponse[index].Type, Is.EqualTo(FolderType.CarNeverRegistered));
                Assert.That(foldersResponse[index].Documents.Count, Is.EqualTo(3));

                Assert.That(foldersResponse[index].Documents[0].DocumentType, Is.EqualTo(DocumentType.IdentityCard));
                Assert.That(foldersResponse[index].Documents[0].StorageUrl, Is.EqualTo($"url-document-{index}-{1}"));

                Assert.That(foldersResponse[index].Documents[1].DocumentType, Is.EqualTo(DocumentType.NotFound));
                Assert.That(foldersResponse[index].Documents[1].StorageUrl, Is.EqualTo($"url-document-{index}-{2}"));

                Assert.That(foldersResponse[index].Documents[2].DocumentType,
                    Is.EqualTo(DocumentType.OwnershipContract));
                Assert.That(foldersResponse[index].Documents[2].StorageUrl, Is.EqualTo($"url-document-{index}-{3}"));
            }
        }
    }
}