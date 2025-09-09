using ContactBook.Domain;
using ContactBook.Queries;
using ContactBook.Repositories;
using MediatR;

namespace ContactBook.Handlers
{
    public class ContactByTypeQueryHandler : IRequestHandler<ContactByTypeQuery, ContactByType>
    {
        private readonly IUserReadRepository _userReadRepository;

        public ContactByTypeQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<ContactByType> Handle(ContactByTypeQuery request, CancellationToken cancellationToken)
        {
            UserContact userContact = await _userReadRepository.GetUserContactAsync(request.UserId);
            var result = userContact.ContactByTypeDictionary[request.ContactType];
            return result;
        }
    }
}