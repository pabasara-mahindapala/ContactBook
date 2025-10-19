using ContactBook.Domain;

namespace ContactBook.Tests.Domain;

public class ContactTests
{
    [Fact]
    public void Contact_CanSetAndGetProperties()
    {
        // Arrange
        var contact = new Contact();
        var id = "contact-1";
        var type = "Email";
        var detail = "john@example.com";
        var userId = "user-1";

        // Act
        contact.Id = id;
        contact.Type = type;
        contact.Detail = detail;
        contact.UserId = userId;

        // Assert
        Assert.Equal(id, contact.Id);
        Assert.Equal(type, contact.Type);
        Assert.Equal(detail, contact.Detail);
        Assert.Equal(userId, contact.UserId);
    }

    [Fact]
    public void Contact_CanBeCreatedWithObjectInitializer()
    {
        // Act
        var contact = new Contact
        {
            Id = "1",
            Type = "Phone",
            Detail = "555-1234",
            UserId = "user-1"
        };

        // Assert
        Assert.Equal("1", contact.Id);
        Assert.Equal("Phone", contact.Type);
        Assert.Equal("555-1234", contact.Detail);
        Assert.Equal("user-1", contact.UserId);
    }
}
