using ContactBook.Domain;

namespace ContactBook.Repositories
{
    public interface IUserRepository
    {
        User Get(string userId);
        void Create(User user);
        void Update(User user);
        void Delete(string userId);
    }
}
