using IUP.Toolkit;

namespace OFG.ChessPeak
{
    public static class PlayerProgress
    {
        public const int LevelsCount = 8;

        public static int CurrentLevel
        {
            get => AdvancedPrefs.GetIntOrDefault(PrefsKey.CurrentLevel, 4);
            set => AdvancedPrefs.SetSaveInt(PrefsKey.CurrentLevel, value);
        }
    }
}
