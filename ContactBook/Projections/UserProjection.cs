using ContactBook.Domain;
using ContactBook.Queries;
using ContactBook.Repositories;

namespace ContactBook.Projections
{
    public class UserProjection
    {
        private readonly IUserReadRepository _userReadRepository;

        public UserProjection(IUserReadRepository userReadRepository)
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
