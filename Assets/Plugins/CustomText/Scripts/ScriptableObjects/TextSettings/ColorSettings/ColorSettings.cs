using System;
using System.Collections.Generic;
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
        public List<ColorParams> Colors;
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

#if UNITY_EDITOR
        protected void OnValidate()
        {
            foreach (var color in Colors)
            {
                color.Name = color.TextColorType.ToString();
            }
            OnColorStyleChanged?.Invoke();
        }
#endif
    }
}