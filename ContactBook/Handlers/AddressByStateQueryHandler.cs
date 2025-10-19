using ContactBook.Domain;
using ContactBook.Queries;
using ContactBook.Repositories;
using MediatR;

namespace ContactBook.Handlers
{
    public class AddressByStateQueryHandler : IRequestHandler<AddressByStateQuery, AddressByState>
    {
        private readonly IUserReadRepository _userReadRepository;

        public AddressByStateQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<AddressByState> Handle(AddressByStateQuery request, CancellationToken cancellationToken)
        {
            UserAddress userAddress = await _userReadRepository.GetUserAddressAsync(request.UserId);
            var result = userAddress.AddressByStateDictionary[request.State];
            return result;
        }
    }
}