using MongoDB.Bson;
using MongoDB.Driver;

namespace Chess.Persistance
{
    public static class PersistanceManager
    {
        public static string Database { get; set; }

        public static IMongoCollection<BsonDocument> GetCollection()
        {
            var client = new MongoClient();
            var db = client.GetDatabase(Database);
            var collection = db.GetCollection<BsonDocument>(Database);
            return collection;
        }
    }
}
