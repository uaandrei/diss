using FenService.Interfaces;

namespace Chess.Infrastructure
{
    public class FenData
    {
        public PieceInfo[] PieceInfos { get; set; }
        public GameInfo GameInfo { get; set; }

        public FenData()
        {
            GameInfo = new GameInfo();
        }
    }
}
