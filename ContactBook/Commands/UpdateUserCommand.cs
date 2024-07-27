using ContactBook.Domain;

namespace ContactBook.Commands
{
    public class UpdateUserCommand
    {
        public string Id { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
