using ContactBook.Domain;
using ContactBook.Repositories;

namespace ContactBook.Projectors
{
    public class UserProjector : IUserProjector
    {
        private readonly IUserReadRepository _userReadRepository;
        public UserProjector(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public void Project(User user)
        {
            UserContact userContact = _userReadRepository.GetUserContact(user.Id);
            foreach (var contact in user.Contacts)
            {
                if (userContact.ContactByTypeDictionary.ContainsKey(contact.Type))
                {
                    var existingContact = userContact.ContactByTypeDictionary[contact.Type];
                    existingContact.Detail = contact.Detail;
                    _userReadRepository.UpdateContactByType(existingContact);
                }
                else
                {
                    var contactByType = new ContactByType
                    {
                        Id = Guid.NewGuid().ToString(),
                        Type = contact.Type,
                        Detail = contact.Detail
                    };
                    _userReadRepository.CreateContactByType(contactByType);
                    _userReadRepository.CreateUserContact(user.Id, contactByType.Id);
                }
            }

            UserAddress userAddress = _userReadRepository.GetUserAddress(user.Id);
            foreach (var address in user.Addresses)
            {
                if (userAddress.AddressByStateDictionary.ContainsKey(address.State))
                {
                    var existingAddress = userAddress.AddressByStateDictionary[address.State];
                    existingAddress.City = address.City;
                    existingAddress.Postcode = address.Postcode;
                    _userReadRepository.UpdateAddressByState(existingAddress);
                }
                else
                {
                    var addressByState = new AddressByState
                    {
                        Id = Guid.NewGuid().ToString(),
                        State = address.State,
                        City = address.City,
                        Postcode = address.Postcode
                    };
                    _userReadRepository.CreateAddressByState(addressByState);
                    _userReadRepository.CreateUserAddress(user.Id, addressByState.Id);
                }
            }
        }
    }
}
