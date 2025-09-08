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

        public Task<AddressByState> Handle(AddressByStateQuery request, CancellationToken cancellationToken)
        {
            UserAddress userAddress = _userReadRepository.GetUserAddress(request.UserId);
            var result = userAddress.AddressByStateDictionary[request.State];
            return Task.FromResult(result);
        }
    }
}