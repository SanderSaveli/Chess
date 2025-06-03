using IUP.Toolkit;

namespace OFG.ChessPeak
{
    public static class PlayerProgress
    {
        public const int LevelsCount = 13;

        public static int GetWorldCurrentLevel(string worldId)
        {
            return AdvancedPrefs.GetIntOrDefault(Const.LEVLES_KEY + worldId, 1);
        }

        public static void SetWorldCurrentLevel(string worldId, int value)
        {
            AdvancedPrefs.SetSaveInt(Const.LEVLES_KEY + worldId, value);
        }
    }
}
