using ContactBook.Domain;

namespace ContactBook.Repositories
{
    public interface IAddressRepository
    {
        Address Get(string addressId);
        void Create(Address address);
        void Update(Address address);
        void Delete(string addressId);
    }
}
