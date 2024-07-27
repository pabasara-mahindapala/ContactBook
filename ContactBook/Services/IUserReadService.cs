using ContactBook.Domain;
using ContactBook.Queries;

namespace ContactBook.Services
{
    public interface IUserReadService
    {
        ContactByType Handle(ContactByTypeQuery query);
        AddressByState Handle(AddressByStateQuery query);
    }
}
