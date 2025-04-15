using UnityEngine;

namespace OFG.ChessPeak
{
    public class StringPrefsValue : PrefsOrDefaultValue<string>
    {
        public StringPrefsValue(string key, string defaultValue) : base(key, defaultValue)
        {
            if (PlayerPrefs.HasKey(key))
            {
                _value = PlayerPrefs.GetString(key);
            }
            else
            {
                SetValue(defaultValue);
            }
        }

        protected override string SetValue(string value)
        {
            PlayerPrefs.SetString(_key, value);
            return value;
        }
    }
}
