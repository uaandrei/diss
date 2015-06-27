using System;
using System.Runtime.Serialization;

namespace SmartChessService.DataContracts
{
    [DataContract]
    public class ChessEngineResult
    {
        public string Id { get; set; }
        [DataMember]
        public int Score { get; set; }
        [DataMember]
        public int FromRank { get; set; }
        [DataMember]
        public char FromFile { get; set; }
        [DataMember]
        public int ToRank { get; set; }
        [DataMember]
        public char ToFile { get; set; }
        [DataMember]
        public char PromotedTo { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }

        public static ChessEngineResult New(string moveResponse)
        {
            var moveResponseItems = moveResponse.Split(';');
            char fromFile = moveResponseItems[0][0];
            int fromRank = moveResponseItems[0][1] - '0';
            char toFile = moveResponseItems[0][2];
            int toRank = moveResponseItems[0][3] - '0';
            char? promotedTo = null;
            if (moveResponseItems[0].Length == 5)
                promotedTo = moveResponseItems[0][4];
            int score = Convert.ToInt32(moveResponseItems[1]);

            return new ChessEngineResult
            {
                Score = score,
                FromFile = fromFile,
                FromRank = fromRank,
                ToFile = toFile,
                ToRank = toRank,
                PromotedTo = promotedTo ?? ' '
            };
        }
    }
}
