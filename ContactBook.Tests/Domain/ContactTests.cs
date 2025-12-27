using ContactBook.Domain;

namespace ContactBook.Tests.Domain;

public class ContactTests
{
    [Test]
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
        Assert.That(contact.Id, Is.EqualTo(id));
        Assert.That(contact.Type, Is.EqualTo(type));
        Assert.That(contact.Detail, Is.EqualTo(detail));
        Assert.That(contact.UserId, Is.EqualTo(userId));
    }

    [Test]
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
        Assert.That(contact.Id, Is.EqualTo("1"));
        Assert.That(contact.Type, Is.EqualTo("Phone"));
        Assert.That(contact.Detail, Is.EqualTo("555-1234"));
        Assert.That(contact.UserId, Is.EqualTo("user-1"));
    }
}
