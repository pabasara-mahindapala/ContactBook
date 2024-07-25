namespace ContactBook.Domain
{
    public class UserAddress
    {
        public string UserId { get; set; }
        public Dictionary<string, AddressByState> AddressByStateDictionary { get; set; }
    }
}
