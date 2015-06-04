using System;
using System.Runtime.InteropServices;

namespace SmartChessService
{
    public class ChessService
    {
        [DllImport("ChessSmartAI.dll", EntryPoint = "GetMove", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetMove(string fen, int depth);

        public string GetMoveResponse(string fen, int depth)
        {
            var move = GetMove(fen, depth);
            string result = Marshal.PtrToStringAnsi(move);
            return result;
        }
    }
}
