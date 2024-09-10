using IUP.Toolkit;
using Singletones;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class ThemeManager : DontDestroyOnLoadSingletone<ThemeManager>
    {
        [SerializeField] private List<ThemeData> _themes = new();
        private ThemeData _actualTheme;
        private int _actualThemeIndex;
        public ThemeData actualTheme => _actualTheme;
        public int actualThemeIndex=> _actualThemeIndex;
        public IReadOnlyList<ThemeData> themes => _themes;

        public override void Awake()
        {
            base.Awake();
            SetActualTheme();
        }
        private void SetActualTheme()
        {
            int actualThemeIndex = AdvancedPrefs.GetIntOrDefault(PrefsKey.ActualTheme, 0);
            SetNewActualTheme(actualThemeIndex);
        }
        public void SetNewActualTheme(int index)
        {
            if (index > _themes.Count - 1)
            {
                LoadDefaultThemeFromResourses();
            }
            else
            {
                SetNewActualTheme(_themes[index], index);
            }
        }

        public void SetNewActualTheme(ThemeData theme, int index)
        {
            _actualThemeIndex = index;
            _actualTheme = theme;

            PlayerPrefs.SetInt(PrefsKey.ActualTheme, index);
            EventNewThemeSet ctx = new EventNewThemeSet(_actualTheme);
            EventBusProvider.EventBus.InvokeEvent(ctx);
        }

        private void LoadDefaultThemeFromResourses()
        {
            ThemeData theme = Resources.Load("DefaulTheme") as ThemeData;
            SetNewActualTheme(theme, 0);
        }
    }
}
