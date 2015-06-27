using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Chess.Persistance
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public User(string name, string pwd)
        {
            Name = name;
            //var bytes = new MD5Cng().ComputeHash(Encoding.UTF8.GetBytes(pwd));
            Password = pwd;
        }

        //public List<string> GetAllSavedGames() { }

        public void SaveGame(string fen) { }

        public static User GetUser(string userName)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("test");
            var collection = db.GetCollection<BsonDocument>("test");
            var filter = Builders<BsonDocument>.Filter.Eq("name", userName);
            using (var cursor = collection.FindAsync(filter).Result)
            {
                while (cursor.MoveNextAsync().Result)
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        var bsonDoc = document.ToBsonDocument();
                        return new User(
                            bsonDoc["name"].AsString,
                            bsonDoc["pwd"].AsString);

                    }
                }
            }
            return null;
        }

        public static void NewUser(string userName, string pwd) { }
    }
}
