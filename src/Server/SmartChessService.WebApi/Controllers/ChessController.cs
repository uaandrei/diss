using System.Web.Http;

namespace SmartChessService.WebApi.Controllers
{
    public class ChessController : ApiController
    {
        public string Get(string fen, int depth)
        {
            var service = new ChessService();
            var moveResponse = service.GetMoveResponse(fen, depth);

            return moveResponse;
        }

        public string Get()
        {
            return "Server State is OK";
        }
    }
}
