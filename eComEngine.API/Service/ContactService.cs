using eComEngine.API.DbContext;
using eComEngine.API.Entitity;
using LiteDB;

namespace eComEngine.API.Service
{
    public class ContactService : IContactService
    {
        private LiteDatabase _liteDb;

        public ContactService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public IEnumerable<ContactEntity> FindAll()
        {
            return _liteDb.GetCollection<ContactEntity>("Contact")
                .FindAll();
        }

        public ContactEntity? Find(int id)
        {
            return _liteDb.GetCollection<ContactEntity>("Contact")
                .Find(x => x.Id == id).FirstOrDefault();
        }

        public int Insert(ContactEntity contact)
        {
            return _liteDb.GetCollection<ContactEntity>("Contact")
                .Insert(contact);
        }

        public bool Update(ContactEntity contact)
        {
            return _liteDb.GetCollection<ContactEntity>("Contact")
                .Update(contact);
        }

        public bool Delete(int id)
        {
            return _liteDb.GetCollection<ContactEntity>("Contact")
                .Delete((BsonValue)id);
        }

        public int DeleteAll()
        {
            return _liteDb.GetCollection<ContactEntity>("Contact")
                .DeleteAll();
        }
    }
}
