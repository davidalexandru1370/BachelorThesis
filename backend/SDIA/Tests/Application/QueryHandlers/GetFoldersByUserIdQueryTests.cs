using System.Security.Claims;
using Application.Query.Folder;
using Infrastructure.DbContext;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SDIA.Configurations;
using SDIA.Controllers;
using SDIA.Security;
using System.Security.Principal;
using Application.Gatherings;
using Application.Interfaces;

namespace Tests.Application.QueryHandlers
{
    class GetFoldersByUserIdQueryTests
    {
        private ServiceProvider _serviceProvider;
        private IMediator _mediator;
        private Guid _userId = Guid.Parse("2cc9f976-60af-4c26-97c9-d935cd37a100");

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<SdiaDbContext>(options => { options.UseInMemoryDatabase("Sdia"); });

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MapsterConfiguration).Assembly);

            _serviceProvider = services.BuildServiceProvider();

            //instantiate the mediator
            var mediator = new ServiceCollection()
                .AddMediatR(options => { options.RegisterServicesFromAssembly(ApplicationAssembly.Assembly); })
                .BuildServiceProvider();
            _mediator = mediator.GetService<IMediator>();
        }

        [TestCase]
        public async Task GetFoldersByUserIdQuery_ValidUserId_ShouldReturnFolders()
        {
            var contextMock = new Mock<HttpContext>();
            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, _userId.ToString())
            }));
            
            contextMock.Setup(x => x.User).Returns(claims);
            var context = new ControllerContext(new ActionContext(contextMock.Object,
                new Microsoft.AspNetCore.Routing.RouteData(),
                new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor()));

            FolderController folderController = new FolderController(_mediator)
            {
                ControllerContext = context
            };

            var result = await folderController.GetFolders();
            int a = 2;
        }
    }
}