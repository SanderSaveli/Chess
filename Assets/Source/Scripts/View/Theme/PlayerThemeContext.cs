using UnityEngine;

namespace OFG.ChessPeak
{
    public class PlayerThemeContext : MonoBehaviour
    {
        public ThemeData Theme;
        public bool IsOpened;

        public PlayerThemeContext(ThemeData themeData, bool isOpend) 
        { 
            Theme = themeData;
            IsOpened = isOpend;
        }
    }
}
