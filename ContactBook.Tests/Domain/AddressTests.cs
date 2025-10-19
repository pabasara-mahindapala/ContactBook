using ContactBook.Domain;

namespace ContactBook.Tests.Domain;

public class AddressTests
{
    [Fact]
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
        Assert.Equal(id, address.Id);
        Assert.Equal(city, address.City);
        Assert.Equal(state, address.State);
        Assert.Equal(postcode, address.Postcode);
        Assert.Equal(userId, address.UserId);
    }

    [Fact]
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
        Assert.Equal("1", address.Id);
        Assert.Equal("Los Angeles", address.City);
        Assert.Equal("CA", address.State);
        Assert.Equal("90001", address.Postcode);
        Assert.Equal("user-1", address.UserId);
    }
}
