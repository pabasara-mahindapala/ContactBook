using ContactBook.Domain;

namespace ContactBook.Projectors
{
    public interface IUserProjector
    {
        Task ProjectAsync(User user);
    }
}
