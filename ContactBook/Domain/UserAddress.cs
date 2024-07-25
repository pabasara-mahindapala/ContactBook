namespace ContactBook.Domain
{
    public class UserAddress
    {
        public string UserId { get; set; }
        public Dictionary<string, Address> AddressByStateDictionary { get; set; }
    }
}
