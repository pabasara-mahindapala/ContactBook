using ContactBook.Domain;

namespace ContactBook.Repositories
{
    public interface IContactRepository
    {
        Contact Get(string contactId);
        void Create(Contact contact);
        void Update(Contact contact);
        void Delete(string contactId);
    }
}
