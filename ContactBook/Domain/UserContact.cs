namespace ContactBook.Domain
{
    public class UserContact
    {
        public string UserId { get; set; }
        public Dictionary<string, Contact> ContactByTypeDictionary { get; set; }
    }
}
