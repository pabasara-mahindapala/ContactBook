using ContactBook.Domain;
using ContactBook.Queries;
using ContactBook.Repositories;

namespace ContactBook.Services
{
    public class UserReadService : IUserReadService
    {
        private readonly IUserReadRepository _userReadRepository;

        public UserReadService(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public ContactByType Handle(ContactByTypeQuery query)
        {
            UserContact userContact = _userReadRepository.GetUserContact(query.UserId);
            return userContact.ContactByTypeDictionary[query.ContactType];
        }

        public AddressByState Handle(AddressByStateQuery query)
        {
            UserAddress userAddress = _userReadRepository.GetUserAddress(query.UserId);
            return userAddress.AddressByStateDictionary[query.State];
        }

    }
}
