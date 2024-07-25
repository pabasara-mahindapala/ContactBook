using ContactBook.Domain;

namespace ContactBook.Repositories
{
    public interface IUserReadRepository
    {
        UserContact GetUserContact(string userId);
        UserAddress GetUserAddress(string userId);
    }
}
