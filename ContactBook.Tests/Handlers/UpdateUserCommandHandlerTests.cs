using ContactBook.Commands;
using ContactBook.Domain;
using ContactBook.Handlers;
using ContactBook.Projectors;
using ContactBook.Repositories;
using Moq;

namespace ContactBook.Tests.Handlers;

public class UpdateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_UpdatesUserContacts_Successfully()
    {
        // Arrange
        var mockRepository = new Mock<IUserWriteRepository>();
        var mockProjector = new Mock<IUserProjector>();
        var handler = new UpdateUserCommandHandler(mockRepository.Object, mockProjector.Object);
        
        var userId = "user-1";
        var existingUser = new User(userId, "John", "Doe");
        var newContact = new Contact { Id = "contact-1", Type = "Email", Detail = "john@example.com" };
        
        var command = new UpdateUserCommand
        {
            Id = userId,
            Contacts = new List<Contact> { newContact },
            Addresses = new List<Address>()
        };

        mockRepository.Setup(r => r.GetAsync(userId)).ReturnsAsync(existingUser);
        mockRepository.Setup(r => r.GetContactAsync(newContact.Id)).ReturnsAsync((Contact)null);
        mockRepository.Setup(r => r.CreateContactAsync(It.IsAny<Contact>())).Returns(Task.CompletedTask);
        mockProjector.Setup(p => p.ProjectAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Contacts);
        Assert.Equal("john@example.com", result.Contacts[0].Detail);
        mockRepository.Verify(r => r.CreateContactAsync(It.Is<Contact>(c => c.UserId == userId)), Times.Once);
        mockProjector.Verify(p => p.ProjectAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task Handle_UpdatesUserAddresses_Successfully()
    {
        // Arrange
        var mockRepository = new Mock<IUserWriteRepository>();
        var mockProjector = new Mock<IUserProjector>();
        var handler = new UpdateUserCommandHandler(mockRepository.Object, mockProjector.Object);
        
        var userId = "user-1";
        var existingUser = new User(userId, "Jane", "Smith");
        var newAddress = new Address { Id = "address-1", City = "New York", State = "NY", Postcode = "10001" };
        
        var command = new UpdateUserCommand
        {
            Id = userId,
            Contacts = new List<Contact>(),
            Addresses = new List<Address> { newAddress }
        };

        mockRepository.Setup(r => r.GetAsync(userId)).ReturnsAsync(existingUser);
        mockRepository.Setup(r => r.GetAddressAsync(newAddress.Id)).ReturnsAsync((Address)null);
        mockRepository.Setup(r => r.CreateAddressAsync(It.IsAny<Address>())).Returns(Task.CompletedTask);
        mockProjector.Setup(p => p.ProjectAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Addresses);
        Assert.Equal("New York", result.Addresses[0].City);
        mockRepository.Verify(r => r.CreateAddressAsync(It.Is<Address>(a => a.UserId == userId)), Times.Once);
    }

    [Fact]
    public async Task Handle_UpdatesExistingContact_WhenContactExists()
    {
        // Arrange
        var mockRepository = new Mock<IUserWriteRepository>();
        var mockProjector = new Mock<IUserProjector>();
        var handler = new UpdateUserCommandHandler(mockRepository.Object, mockProjector.Object);
        
        var userId = "user-1";
        var existingUser = new User(userId, "Bob", "Wilson");
        var existingContact = new Contact { Id = "contact-1", Type = "Email", Detail = "old@example.com" };
        var updatedContact = new Contact { Id = "contact-1", Type = "Email", Detail = "new@example.com" };
        
        var command = new UpdateUserCommand
        {
            Id = userId,
            Contacts = new List<Contact> { updatedContact },
            Addresses = new List<Address>()
        };

        mockRepository.Setup(r => r.GetAsync(userId)).ReturnsAsync(existingUser);
        mockRepository.Setup(r => r.GetContactAsync(updatedContact.Id)).ReturnsAsync(existingContact);
        mockRepository.Setup(r => r.UpdateContactAsync(It.IsAny<Contact>())).Returns(Task.CompletedTask);
        mockProjector.Setup(p => p.ProjectAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.UpdateContactAsync(It.Is<Contact>(c => 
            c.Id == "contact-1" && c.Detail == "new@example.com" && c.UserId == userId)), Times.Once);
        mockRepository.Verify(r => r.CreateContactAsync(It.IsAny<Contact>()), Times.Never);
    }

    [Fact]
    public async Task Handle_UpdatesExistingAddress_WhenAddressExists()
    {
        // Arrange
        var mockRepository = new Mock<IUserWriteRepository>();
        var mockProjector = new Mock<IUserProjector>();
        var handler = new UpdateUserCommandHandler(mockRepository.Object, mockProjector.Object);
        
        var userId = "user-1";
        var existingUser = new User(userId, "Alice", "Brown");
        var existingAddress = new Address { Id = "address-1", City = "Boston", State = "MA", Postcode = "02101" };
        var updatedAddress = new Address { Id = "address-1", City = "Cambridge", State = "MA", Postcode = "02139" };
        
        var command = new UpdateUserCommand
        {
            Id = userId,
            Contacts = new List<Contact>(),
            Addresses = new List<Address> { updatedAddress }
        };

        mockRepository.Setup(r => r.GetAsync(userId)).ReturnsAsync(existingUser);
        mockRepository.Setup(r => r.GetAddressAsync(updatedAddress.Id)).ReturnsAsync(existingAddress);
        mockRepository.Setup(r => r.UpdateAddressAsync(It.IsAny<Address>())).Returns(Task.CompletedTask);
        mockProjector.Setup(p => p.ProjectAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.UpdateAddressAsync(It.Is<Address>(a => 
            a.Id == "address-1" && a.City == "Cambridge" && a.UserId == userId)), Times.Once);
        mockRepository.Verify(r => r.CreateAddressAsync(It.IsAny<Address>()), Times.Never);
    }
}
