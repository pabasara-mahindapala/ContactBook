using ContactBook.Domain;
using MediatR;

namespace ContactBook.Queries
{
    public class AddressByStateQuery : IRequest<AddressByState>
    {
        public string UserId { get; set; }
        public string State { get; set; }
    }
}
