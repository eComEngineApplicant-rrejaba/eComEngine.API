using LiteDB;

namespace eComEngine.API.DbContext
{
    public class LiteDbContext : ILiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext()
        {
            Database = new LiteDatabase("LiteDb/eComEngine.db");
        }
    }
}
