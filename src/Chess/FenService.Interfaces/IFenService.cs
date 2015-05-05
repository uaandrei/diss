namespace FenService.Interfaces
{
    public interface IFenService
    {
        string GetFen(IFenData fenData);
        IFenData GetData(string fen);
    }
}
