using ContactBook.Domain;

namespace ContactBook.Tests.Domain;

public class UserTests
{
    [Fact]
    public void Constructor_SetsProperties_Correctly()
    {
        // Arrange
        var id = "test-id";
        var firstName = "John";
        var lastName = "Doe";

        // Act
        var user = new User(id, firstName, lastName);

        // Assert
        Assert.Equal(id, user.Id);
        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);
    }

    [Fact]
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
        Assert.Equal(newId, user.Id);
        Assert.Equal(newFirstName, user.FirstName);
        Assert.Equal(newLastName, user.LastName);
    }

    [Fact]
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
        Assert.NotNull(user.Contacts);
        Assert.Single(user.Contacts);
        Assert.Equal("Email", user.Contacts[0].Type);
    }

    [Fact]
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
        Assert.NotNull(user.Addresses);
        Assert.Single(user.Addresses);
        Assert.Equal("New York", user.Addresses[0].City);
    }
}
