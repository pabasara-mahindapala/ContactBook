using ContactBook.Commands;
using ContactBook.Domain;
using ContactBook.Projectors;
using ContactBook.Repositories;

namespace ContactBook.Aggregates
{
    public class UserAggregate
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserProjector _userProjector;

        public UserAggregate(
            IUserWriteRepository userWriteRepository,
            IUserProjector userProjector)
        {
            _userWriteRepository = userWriteRepository;
            _userProjector = userProjector;
        }

        public User HandleCreateUserCommand(CreateUserCommand command)
        {
            User user = new User(command.Id, command.FirstName, command.LastName);
            _userWriteRepository.Create(user);
            return user;
        }

        public User HandleUpdateUserCommand(UpdateUserCommand command)
        {
            User user = _userWriteRepository.Get(command.Id);            
            user.Contacts = UpdateContacts(user, command.Contacts);
            user.Addresses = UpdateAddresses(user, command.Addresses);
            _userProjector.Project(user);
            return user;
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
