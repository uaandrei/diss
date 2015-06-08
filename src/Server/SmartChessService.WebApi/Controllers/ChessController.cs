﻿using System;
using System.Web.Http;

namespace SmartChessService.WebApi.Controllers
{
    public class ChessController : ApiController
    {
        public string Get(string fen, int depth)
        {
            try
            {

                Console.WriteLine("{0} - parsing fen\n{1}", DateTime.Now, fen);
                var service = new ChessService();
                var moveResponse = service.GetMoveResponse(fen, depth);
                Console.WriteLine("{0} - generated move:{1}", DateTime.Now, moveResponse);

                return moveResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "error occured";
            }
        }

        public string Get()
        {
            return "Server State is OK";
        }
    }
}
