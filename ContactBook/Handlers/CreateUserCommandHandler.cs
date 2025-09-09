using ContactBook.Commands;
using ContactBook.Domain;
using ContactBook.Repositories;
using MediatR;

namespace ContactBook.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserWriteRepository _userWriteRepository;

        public CreateUserCommandHandler(IUserWriteRepository userWriteRepository)
        {
            _userWriteRepository = userWriteRepository;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User(Guid.NewGuid().ToString(), request.FirstName, request.LastName);
            await _userWriteRepository.CreateAsync(user);
            return user;
        }
    }
}