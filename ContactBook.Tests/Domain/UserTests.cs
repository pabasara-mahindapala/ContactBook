using ContactBook.Domain;

namespace ContactBook.Tests.Domain;

public class UserTests
{
    [Test]
    public void Constructor_SetsProperties_Correctly()
    {
        // Arrange
        var id = "test-id";
        var firstName = "John";
        var lastName = "Doe";

        // Act
        var user = new User(id, firstName, lastName);

        // Assert
        Assert.That(user.Id, Is.EqualTo(id));
        Assert.That(user.FirstName, Is.EqualTo(firstName));
        Assert.That(user.LastName, Is.EqualTo(lastName));
    }

    [Test]
    public void User_CanSetAndGetProperties()
    {
        // Arrange
        var user = new User("1", "John", "Doe");
        var newId = "2";
        var newFirstName = "Jane";
        var newLastName = "Smith";

        // Act
        user.Id = newId;
        user.FirstName = newFirstName;
        user.LastName = newLastName;

        // Assert
        Assert.That(user.Id, Is.EqualTo(newId));
        Assert.That(user.FirstName, Is.EqualTo(newFirstName));
        Assert.That(user.LastName, Is.EqualTo(newLastName));
    }

    [Test]
    public void User_CanAssignContacts()
    {
        // Arrange
        var user = new User("1", "John", "Doe");
        var contacts = new List<Contact>
        {
            new Contact { Id = "1", Type = "Email", Detail = "john@example.com", UserId = "1" }
        };

        // Act
        user.Contacts = contacts;

        // Assert
        Assert.That(user.Contacts, Is.Not.Null);
        Assert.That(user.Contacts, Has.Count.EqualTo(1));
        Assert.That(user.Contacts[0].Type, Is.EqualTo("Email"));
    }

    [Test]
    public void User_CanAssignAddresses()
    {
        // Arrange
        var user = new User("1", "John", "Doe");
        var addresses = new List<Address>
        {
            new Address { Id = "1", City = "New York", State = "NY", Postcode = "10001", UserId = "1" }
        };

        // Act
        user.Addresses = addresses;

        // Assert
        Assert.That(user.Addresses, Is.Not.Null);
        Assert.That(user.Addresses, Has.Count.EqualTo(1));
        Assert.That(user.Addresses[0].City, Is.EqualTo("New York"));
    }
}
