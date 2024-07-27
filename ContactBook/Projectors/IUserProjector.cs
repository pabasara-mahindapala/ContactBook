using ContactBook.Domain;

namespace ContactBook.Projectors
{
    public interface IUserProjector
    {
        void Project(User user);
    }
}
