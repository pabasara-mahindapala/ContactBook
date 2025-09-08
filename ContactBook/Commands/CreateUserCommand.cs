using ContactBook.Domain;
using MediatR;

namespace ContactBook.Commands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
