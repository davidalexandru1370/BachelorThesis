using Application.Commands.Folder;

namespace Tests.Application.CommandHandlers;

public class CreateFolderHandlerTests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void CreateFolderHandler_ValidEntity_ShouldCreateFolder()
    {
        // Arrange
        var createFolderCommand = new CreateFolderCommand()
        {

        };
    }
}