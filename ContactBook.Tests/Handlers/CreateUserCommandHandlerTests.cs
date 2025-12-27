using ContactBook.Commands;
using ContactBook.Domain;
using ContactBook.Handlers;
using ContactBook.Repositories;
using Moq;

namespace ContactBook.Tests.Handlers;

public class CreateUserCommandHandlerTests
{
    [Test]
    public async Task Handle_CreatesUser_WithCorrectProperties()
    {
        // Arrange
        var mockRepository = new Mock<IUserWriteRepository>();
        var handler = new CreateUserCommandHandler(mockRepository.Object);
        var command = new CreateUserCommand
        {
            FirstName = "John",
            LastName = "Doe"
        };

        User createdUser = null;
        mockRepository.Setup(r => r.CreateAsync(It.IsAny<User>()))
            .Callback<User>(u => createdUser = u)
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FirstName, Is.EqualTo("John"));
        Assert.That(result.LastName, Is.EqualTo("Doe"));
        Assert.That(string.IsNullOrEmpty(result.Id), Is.False);
        mockRepository.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Once);
    }

    [Test]
    public async Task Handle_GeneratesUniqueId_ForEachUser()
    {
        // Arrange
        var mockRepository = new Mock<IUserWriteRepository>();
        var handler = new CreateUserCommandHandler(mockRepository.Object);
        var command1 = new CreateUserCommand { FirstName = "John", LastName = "Doe" };
        var command2 = new CreateUserCommand { FirstName = "Jane", LastName = "Smith" };

        mockRepository.Setup(r => r.CreateAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        // Act
        var result1 = await handler.Handle(command1, CancellationToken.None);
        var result2 = await handler.Handle(command2, CancellationToken.None);

        // Assert
        Assert.That(result1.Id, Is.Not.EqualTo(result2.Id));
    }

    [Test]
    public async Task Handle_CallsRepository_WithCorrectUser()
    {
        // Arrange
        var mockRepository = new Mock<IUserWriteRepository>();
        var handler = new CreateUserCommandHandler(mockRepository.Object);
        var command = new CreateUserCommand
        {
            FirstName = "Alice",
            LastName = "Johnson"
        };

        User capturedUser = null;
        mockRepository.Setup(r => r.CreateAsync(It.IsAny<User>()))
            .Callback<User>(u => capturedUser = u)
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(capturedUser, Is.Not.Null);
        Assert.That(capturedUser.Id, Is.EqualTo(result.Id));
        Assert.That(capturedUser.FirstName, Is.EqualTo("Alice"));
        Assert.That(capturedUser.LastName, Is.EqualTo("Johnson"));
    }
}
