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

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userWriteRepository.GetAsync(request.Id);
            user.Contacts = await UpdateContactsAsync(user, request.Contacts);
            user.Addresses = await UpdateAddressesAsync(user, request.Addresses);
            await _userProjector.ProjectAsync(user);
            return user;
        }

        private async Task<List<Address>> UpdateAddressesAsync(User user, List<Address> addresses)
        {
            foreach (var address in addresses)
            {
                address.UserId = user.Id;
                var existingAddress = await _userWriteRepository.GetAddressAsync(address.Id);
                if (existingAddress != null)
                {
                    await _userWriteRepository.UpdateAddressAsync(address);
                }
                else
                {
                    await _userWriteRepository.CreateAddressAsync(address);
                }
            }
            return addresses;
        }

        private async Task<List<Contact>> UpdateContactsAsync(User user, List<Contact> contacts)
        {
            foreach (var contact in contacts)
            {
                contact.UserId = user.Id;
                var existingContact = await _userWriteRepository.GetContactAsync(contact.Id);
                if (existingContact != null)
                {
                    await _userWriteRepository.UpdateContactAsync(contact);
                }
                else
                {
                    await _userWriteRepository.CreateContactAsync(contact);
                }
            }
            return contacts;
        }
    }
}