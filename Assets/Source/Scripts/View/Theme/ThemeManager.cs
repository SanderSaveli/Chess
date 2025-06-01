using CustomText;
using IUP.Toolkit;
using Singletones;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Colors;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class ThemeManager : MonoBehaviour
    {
        [SerializeField] private List<ThemeData> _themes = new();
        private ThemeData _actualTheme;
        private int _actualThemeIndex;
        private ColorSettings _colorData;
        public ThemeData actualTheme => _actualTheme;
        public int actualThemeIndex=> _actualThemeIndex;
        public IReadOnlyList<ThemeData> themes => _themes;

        public void Awake()
        {
            _colorData = ColorSettings.Instance;
            if (!PlayerPrefs.HasKey(GetThemeKey(0))){
                InstantThemeKeys();
            }
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

            _colorData.ChangeColors(theme.Colors);
            PlayerPrefs.SetInt(PrefsKey.ActualTheme, index);
            EventNewThemeSet ctx = new EventNewThemeSet(_actualTheme);
            EventBusProvider.EventBus.InvokeEvent(ctx);
        }

        public List<PlayerThemeContext> GetThemeContext()
        {
            int i = 0;
            List<PlayerThemeContext> contexts = new List<PlayerThemeContext>();
            foreach (var theme in _themes)
            {
                contexts.Add( new PlayerThemeContext( theme, PlayerPrefs.GetInt(GetThemeKey(i)) == 1));
                i++;
            }
            return contexts;
        }

        private void LoadDefaultThemeFromResourses()
        {
            ThemeData theme = Resources.Load("DefaultTheme") as ThemeData;
            if (theme == null)
            {
                Debug.LogWarning("Cant load Default Theme");
            }
            SetNewActualTheme(theme, 0);
        }

        private string GetThemeKey(int themeIndex)
        {
            return Const.THEME_KEY + _themes[themeIndex].Name;
        }
        private void InstantThemeKeys()
        {
            int i = 0;
            foreach(var theme in _themes)
            {
                PlayerPrefs.SetInt(GetThemeKey(i), 0);
                i++;
            }
            PlayerPrefs.SetInt(GetThemeKey(0), 1);
        }
    }
}
