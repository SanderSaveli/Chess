using UnityEngine;

namespace IUP.Toolkit
{
    public static class AdvancedPrefs
    {
        public static bool GetBool(string key)
        {
            int value = PlayerPrefs.GetInt(key);
            return value == 1;
        }

        public static void SetBool(string key, bool value)
        {
            int intValue = value ? 1 : 0;
            PlayerPrefs.SetInt(key, intValue);
        }

        public static void SetSaveBool(string key, bool value)
        {
            SetBool(key, value);
            PlayerPrefs.Save();
        }

        public static void SetSaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public static void SetSaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public static void SetSaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        public static float GetFloatOrDefault(string key, float defaultValue)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetFloat(key);
            }
            return defaultValue;
        }

        public static int GetIntOrDefault(string key, int defaultValue)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }
            return defaultValue;
        }

        public static string GetStringOrDefault(string key, string defaultValue)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetString(key);
            }
            return defaultValue;
        }

        public static bool GetBoolOrDefault(string key, bool defaultValue)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return GetBool(key);
            }
            return defaultValue;
        }
    }
}
