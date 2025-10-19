using ContactBook.Domain;

namespace ContactBook.Tests.Domain;

public class AddressTests
{
    [Test]
    public void Address_CanSetAndGetProperties()
    {
        // Arrange
        var address = new Address();
        var id = "address-1";
        var city = "New York";
        var state = "NY";
        var postcode = "10001";
        var userId = "user-1";

        // Act
        address.Id = id;
        address.City = city;
        address.State = state;
        address.Postcode = postcode;
        address.UserId = userId;

        // Assert
        Assert.That(address.Id, Is.EqualTo(id));
        Assert.That(address.City, Is.EqualTo(city));
        Assert.That(address.State, Is.EqualTo(state));
        Assert.That(address.Postcode, Is.EqualTo(postcode));
        Assert.That(address.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void Address_CanBeCreatedWithObjectInitializer()
    {
        // Act
        var address = new Address
        {
            Id = "1",
            City = "Los Angeles",
            State = "CA",
            Postcode = "90001",
            UserId = "user-1"
        };

        // Assert
        Assert.That(address.Id, Is.EqualTo("1"));
        Assert.That(address.City, Is.EqualTo("Los Angeles"));
        Assert.That(address.State, Is.EqualTo("CA"));
        Assert.That(address.Postcode, Is.EqualTo("90001"));
        Assert.That(address.UserId, Is.EqualTo("user-1"));
    }
}
