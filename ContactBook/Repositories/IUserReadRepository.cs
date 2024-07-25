using ContactBook.Domain;

namespace ContactBook.Repositories
{
    public interface IUserReadRepository
    {
        UserContact GetUserContact(string userId);
        UserAddress GetUserAddress(string userId);
        void CreateUserContact(string userId, string contactByTypeId);
        void CreateContactByType(ContactByType contactByType);
        void UpdateContactByType(ContactByType existingContact);
        void CreateUserAddress(string id1, string id2);
        void CreateAddressByState(AddressByState addressByState);
        void UpdateAddressByState(AddressByState existingAddress);
    }
}
