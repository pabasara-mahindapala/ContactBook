using ContactBook.Domain;
using MediatR;

namespace ContactBook.Commands
{
    public class UpdateUserCommand : IRequest<User>
    {
        public string Id { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
