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

        public async Task ProjectAsync(User user)
        {
            UserContact userContact = await _userReadRepository.GetUserContactAsync(user.Id);
            foreach (var contact in user.Contacts)
            {
                if (userContact.ContactByTypeDictionary.ContainsKey(contact.Type))
                {
                    var existingContact = userContact.ContactByTypeDictionary[contact.Type];
                    existingContact.Detail = contact.Detail;
                    await _userReadRepository.UpdateContactByTypeAsync(existingContact);
                }
                else
                {
                    var contactByType = new ContactByType
                    {
                        Id = contact.Id ?? Guid.NewGuid().ToString(),
                        Type = contact.Type,
                        Detail = contact.Detail
                    };
                    await _userReadRepository.CreateContactByTypeAsync(contactByType);
                    await _userReadRepository.CreateUserContactAsync(user.Id, contactByType.Id);
                }
            }

            UserAddress userAddress = await _userReadRepository.GetUserAddressAsync(user.Id);
            foreach (var address in user.Addresses)
            {
                if (userAddress.AddressByStateDictionary.ContainsKey(address.State))
                {
                    var existingAddress = userAddress.AddressByStateDictionary[address.State];
                    existingAddress.City = address.City;
                    existingAddress.Postcode = address.Postcode;
                    await _userReadRepository.UpdateAddressByStateAsync(existingAddress);
                }
                else
                {
                    var addressByState = new AddressByState
                    {
                        Id =  address.Id ?? Guid.NewGuid().ToString(),
                        State = address.State,
                        City = address.City,
                        Postcode = address.Postcode
                    };
                    await _userReadRepository.CreateAddressByStateAsync(addressByState);
                    await _userReadRepository.CreateUserAddressAsync(user.Id, addressByState.Id);
                }
            }
        }
    }
}
