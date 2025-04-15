using System;

namespace OFG.ChessPeak
{
    public interface IAccountManager
    {
        public string Name { get; }
        public bool IsInAccount { get; }

        public void Register(string username, string password, Action succsess, Action error);
        public void Login(string username, string password, Action succsess, Action error);
        public void Logout();
    }
}
