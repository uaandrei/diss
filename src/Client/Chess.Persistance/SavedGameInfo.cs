using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Persistance
{
    public class SavedGameInfo
    {
        public string Id { get; set; }
        public DateTime? LastSaved { get; set; }
        public string Fen { get; set; }
        public string Comment { get; set; }

        public void CopyFrom(SavedGameInfo gi)
        {
            Id = gi.Id;
            LastSaved = gi.LastSaved;
            Fen = gi.Fen;
            Comment = gi.Comment;
        }
    }

    public static class GameInfoExtensions
    {
        public static BsonDocument ToBsonDoc(this SavedGameInfo gameInfo)
        {
            return new BsonDocument{
                {"id", gameInfo.Id},
                {"date", gameInfo.LastSaved.Value.ToString("dd-MM-yyyy HH:mm:ss")},
                {"fen", gameInfo.Fen},
                {"comment", gameInfo.Comment}
            };
        }
    }
}
