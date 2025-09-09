using ContactBook.Domain;

namespace ContactBook.Repositories
{
    public interface IUserWriteRepository
    {
        Task<User> GetAsync(string userId);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string userId);
        Task<Contact?> GetContactAsync(string contactId);
        Task CreateContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(string contactId);
        Task<Address?> GetAddressAsync(string addressId);
        Task CreateAddressAsync(Address address);
        Task UpdateAddressAsync(Address address);
        Task DeleteAddressAsync(string addressId);
    }
}
