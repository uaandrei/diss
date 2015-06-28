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
        public List<SavedGameInfo> SavedGames { get; private set; }

        public User(string name, string pwd)
        {
            Name = name;
            Password = pwd;
            SavedGames = new List<SavedGameInfo>();
        }

        public void SaveGame(SavedGameInfo gameInfo)
        {
            if (SavedGames.Any(g => g.Id == gameInfo.Id))
                UpdateGame(gameInfo);
            else
                AddGame(gameInfo);
        }

        public void ForceReloadSavedGames()
        {
            SavedGames.Clear();
            SavedGames.AddRange(GetSavedGames(GetUserBson(Name)));
        }

        public void DeleteGame(SavedGameInfo gameInfo)
        {
            var users = PersistanceManager.GetCollection();

            var x = users.UpdateOneAsync(
                Builders<BsonDocument>.Filter.Eq("name", Name),
                Builders<BsonDocument>.Update.Pull("savedGames", new BsonDocument { { "id", gameInfo.Id } })
            ).Result;
            SavedGames.RemoveAll(g => g.Id == gameInfo.Id);
        }

        private void AddGame(SavedGameInfo gameInfo)
        {
            var users = PersistanceManager.GetCollection();

            var x = users.UpdateOneAsync(
                Builders<BsonDocument>.Filter.Eq("name", Name),
                Builders<BsonDocument>.Update.Push("savedGames", gameInfo.ToBsonDoc())
            ).Result;

            SavedGames.Add(gameInfo);
        }

        private void UpdateGame(SavedGameInfo gameInfo)
        {
            DeleteGame(gameInfo);
            AddGame(gameInfo);
        }

        public static User GetUser(string userName)
        {
            var userBson = GetUserBson(userName);
            if (userBson == null)
                return null;
            var user = new User(
                userBson["name"].AsString,
                userBson["pwd"].AsString);
            var savedGames = GetSavedGames(userBson);
            user.SavedGames.AddRange(savedGames);
            return user;
        }

        public static User NewUser(string userName, string pwd)
        {
            var existingUser = GetUser(userName);
            if (existingUser != null)
            {
                throw new ArgumentException("username used");
            }
            var user = new User(userName, pwd);
            var users = PersistanceManager.GetCollection();
            var x = users.InsertOneAsync(
                new BsonDocument
                {
                    {"name", userName},
                    {"pwd", pwd},
                    {"savedGames", new BsonArray()}
                }
            );
            return user;
        }

        private static List<SavedGameInfo> GetSavedGames(BsonDocument userBson)
        {
            var savedGames = new List<SavedGameInfo>();
            var savedGamesBson = userBson.Elements.First(e => e.Name == "savedGames");
            var savedGamesBsonArray = savedGamesBson.Value.AsBsonArray;
            foreach (var savedGameBson in savedGamesBsonArray)
            {
                var gameInfo = new SavedGameInfo
                {
                    Id = savedGameBson["id"].AsString,
                    Comment = savedGameBson["comment"].AsString,
                    Fen = savedGameBson["fen"].AsString,
                    LastSaved = DateTime.Parse(savedGameBson["date"].AsString)
                };
                savedGames.Add(gameInfo);
            }
            return savedGames;
        }

        private static BsonDocument GetUserBson(string name)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("name", name);
            using (var cursor = PersistanceManager.GetCollection().FindAsync(filter).Result)
            {
                while (cursor.MoveNextAsync().Result)
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        return document;
                    }
                }
            }
            return null;
        }
    }
}
