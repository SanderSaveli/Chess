using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CustomText
{
    [Serializable]
    public class ColorParams
    {
        [HideInInspector] public string Name;
        public Custom_ColorStyle TextColorType;
        public Color Color;
    }

    [CreateAssetMenu(fileName = "ColorSettings", menuName = "CustomText/Settings/ColorSettings", order = 0)]
    public class ColorSettings : ColorSettingsScriptableObject
    {
        public List<ColorParams> Colors => _defaultColors;
        public List<ColorParams> _defaultColors;
        public event Action OnColorStyleChanged;

        private static ColorSettings _instance;
        public static ColorSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<ColorSettings>("TextSettings/ColorSettings");

                    if (_instance == null)
                    {
                        Debug.Log("<color=red>Attention!</color> ColorSettings NOT exist");
                    }
                }

                return _instance;
            }
        }

        public void ChangeColors(List<ColorParams> colors)
        {
            _defaultColors = colors;
            OnColorStyleChanged?.Invoke();
        }

        public Color GetColor(Custom_ColorStyle type)
        {
            return Colors.Find(t => t.TextColorType.Equals(type)).Color;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_defaultColors == null)
                _defaultColors = new List<ColorParams>();

            var enumValues = System.Enum.GetValues(typeof(Custom_ColorStyle)).Cast<Custom_ColorStyle>();

            foreach (var value in enumValues)
            {
                if (!_defaultColors.Any(c => c.TextColorType == value))
                {
                    _defaultColors.Add(new ColorParams
                    {
                        Name = value.ToString(),
                        TextColorType = value,
                        Color = Color.white
                    });
                }
            }

            foreach (var colorParam in _defaultColors)
            {
                colorParam.Name = colorParam.TextColorType.ToString();
            }

            EditorUtility.SetDirty(this);
        }
#endif
    }
}