namespace ContactBook.Domain
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
