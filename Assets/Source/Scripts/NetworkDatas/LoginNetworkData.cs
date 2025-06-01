using System;

namespace OFG.ChessPeak
{
    [Serializable]
    public class LoginNetworkData
    {
        public string name;
        public string password;

        public LoginNetworkData(string name, string password)
        {
            this.name = name;
            this.password = password;
        }
        public LoginNetworkData() { }
    }
}
