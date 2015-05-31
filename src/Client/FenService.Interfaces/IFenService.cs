using Chess.Infrastructure;
namespace FenService.Interfaces
{
    public interface IFenService
    {
        string GetFen(FenData fenData);
        FenData GetData(string fen);
    }
}
