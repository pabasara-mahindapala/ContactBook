using ContactBook.Commands;
using ContactBook.Domain;

namespace ContactBook.Services
{
    public interface IUserWriteService
    {
        User HandleCreateUserCommand(CreateUserCommand command);
        User HandleUpdateUserCommand(UpdateUserCommand command);
    }
}
