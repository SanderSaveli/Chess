using System.Collections.Generic;

namespace OFG.ChessPeak
{
    public class ShopThemeFiller : ItemFiller<ThemeShopScrollElement, PlayerThemeContext>
    {
        public List<ThemeShopScrollElement> Elements => _slots;
    }
}
