using LiteDB;

namespace eComEngine.API.DbContext
{
    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }
}