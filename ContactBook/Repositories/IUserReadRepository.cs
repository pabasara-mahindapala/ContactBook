using ContactBook.Domain;

namespace ContactBook.Repositories
{
    public interface IUserReadRepository
    {
        Task<UserContact> GetUserContactAsync(string userId);
        Task<UserAddress> GetUserAddressAsync(string userId);
        Task CreateUserContactAsync(string userId, string contactByTypeId);
        Task CreateContactByTypeAsync(ContactByType contactByType);
        Task UpdateContactByTypeAsync(ContactByType existingContact);
        Task CreateUserAddressAsync(string id1, string id2);
        Task CreateAddressByStateAsync(AddressByState addressByState);
        Task UpdateAddressByStateAsync(AddressByState existingAddress);
    }
}
