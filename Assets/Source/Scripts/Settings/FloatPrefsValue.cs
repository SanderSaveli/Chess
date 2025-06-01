using UnityEngine;

namespace OFG.ChessPeak
{
    public class FloatPrefsValue : PrefsOrDefaultValue<float>
    {
        public FloatPrefsValue(string key, float defaultValue) : base(key, defaultValue)
        {
            if (PlayerPrefs.HasKey(key))
            {
                _value = PlayerPrefs.GetFloat(key);
            }
            else
            {
                SetValue(defaultValue);
            }
        }

        protected override float SetValue(float value)
        {
            PlayerPrefs.SetFloat(_key, value);
            return value;
        }
    }
}
