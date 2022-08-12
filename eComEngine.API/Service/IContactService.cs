using eComEngine.API.Entitity;

namespace eComEngine.API.Service
{
    public interface IContactService
    {
        bool Delete(int id);
        ContactEntity Find(int id);
        IEnumerable<ContactEntity> FindAll();
        int Insert(ContactEntity contact);
        bool Update(ContactEntity contact);
        int DeleteAll();
    }
}