using ContactBook.Domain;
using MediatR;

namespace ContactBook.Queries
{
    public class ContactByTypeQuery : IRequest<ContactByType>
    {
        public string UserId { get; set; }
        public string ContactType { get; set; }
    }
}
