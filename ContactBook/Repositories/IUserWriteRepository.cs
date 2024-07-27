using ContactBook.Domain;

namespace ContactBook.Repositories
{
    public interface IUserWriteRepository
    {
        User Get(string userId);
        void Create(User user);
        void Update(User user);
        void Delete(string userId);
        Contact GetContact(string contactId);
        void CreateContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(string contactId);
        Address GetAddress(string addressId);
        void CreateAddress(Address address);
        void UpdateAddress(Address address);
        void DeleteAddress(string addressId);
    }
}
