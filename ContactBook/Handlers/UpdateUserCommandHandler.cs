using ContactBook.Commands;
using ContactBook.Domain;
using ContactBook.Projectors;
using ContactBook.Repositories;
using MediatR;

namespace ContactBook.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserProjector _userProjector;

        public UpdateUserCommandHandler(
            IUserWriteRepository userWriteRepository,
            IUserProjector userProjector)
        {
            _userWriteRepository = userWriteRepository;
            _userProjector = userProjector;
        }

        public Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = _userWriteRepository.Get(request.Id);
            user.Contacts = UpdateContacts(user, request.Contacts);
            user.Addresses = UpdateAddresses(user, request.Addresses);
            _userProjector.Project(user);
            return Task.FromResult(user);
        }

        private List<Address> UpdateAddresses(User user, List<Address> addresses)
        {
            foreach (var address in addresses)
            {
                address.UserId = user.Id;
                var existingAddress = _userWriteRepository.GetAddress(address.Id);
                if (existingAddress != null)
                {
                    _userWriteRepository.UpdateAddress(address);
                }
                else
                {
                    _userWriteRepository.CreateAddress(address);
                }
            }
            return addresses;
        }

        private List<Contact> UpdateContacts(User user, List<Contact> contacts)
        {
            foreach (var contact in contacts)
            {
                contact.UserId = user.Id;
                var existingContact = _userWriteRepository.GetContact(contact.Id);
                if (existingContact != null)
                {
                    _userWriteRepository.UpdateContact(contact);
                }
                else
                {
                    _userWriteRepository.CreateContact(contact);
                }
            }
            return contacts;
        }
    }
}