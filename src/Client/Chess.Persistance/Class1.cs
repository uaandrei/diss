using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Persistance
{
    public class User
    {
        public string Name { get; set; }
        private string _password;

        public User(string name, string pwd)
        {
            Name = name;
            var bytes = new MD5Cng().ComputeHash(Encoding.UTF8.GetBytes(pwd));
            _password = bytes.ToString();
        }

        //public List<string> GetAllSavedGames() { }

        public void SaveGame(string fen) { }

        //public static User GetUser(string userName, string pwd) { }

        public static void NewUser(string userName, string pwd) { }
    }
}
