using UnityEngine;

namespace OFG.ChessPeak
{
    public class IntPrefsValue : PrefsOrDefaultValue<int>
    {
        public IntPrefsValue(string key, int defaultValue) : base(key, defaultValue)
        {
            if (PlayerPrefs.HasKey(key))
            {
                _value = PlayerPrefs.GetInt(key);
            }
            else
            {
                SetValue(defaultValue);
            }
        }

        protected override int SetValue(int value)
        {
            PlayerPrefs.SetInt(_key, value);
            _value = value;
            return value;
        }
    }
}
