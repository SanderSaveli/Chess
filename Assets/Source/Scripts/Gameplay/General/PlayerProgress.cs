using IUP.Toolkit;

namespace OFG.ChessPeak
{
    public static class PlayerProgress
    {
        public const int LevelsCount = 13;

        public static int CurrentLevel
        {
            get => AdvancedPrefs.GetIntOrDefault(PrefsKey.CurrentLevel, 1);
            set => AdvancedPrefs.SetSaveInt(PrefsKey.CurrentLevel, value);
        }
    }
}
