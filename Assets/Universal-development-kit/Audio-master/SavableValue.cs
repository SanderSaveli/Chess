using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace UDK.Audio
{
    [Serializable]
    public sealed class SavableValue<T>
    {
        private readonly string _playerPrefsPath;
        private T _value;
        private T _prevValue;
		#pragma warning disable
        public event Action OnChanged = () => { };

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                this._prevValue = this._value;
                this._value = value;
                SaveToPrefs();
                OnChanged.Invoke();
            }
        }

        public T PrevValue
        {
            get { return _prevValue; }
        }

        public SavableValue(string playerPrefsPath, T defaultValue = default(T))
        {
			if (string.IsNullOrEmpty(playerPrefsPath))
				throw new Exception("empty playerPrefsPath in savebleValue");

            this._playerPrefsPath = playerPrefsPath;

            _value = defaultValue;
            _prevValue = defaultValue;

            LoadFromPrefs();
        }

        private void LoadFromPrefs()
        {
            if (!PlayerPrefs.HasKey(_playerPrefsPath))
            {
                SaveToPrefs();
                return;
            }

            var stringToDeserialize = PlayerPrefs.GetString(_playerPrefsPath, "");

            var bytes = Convert.FromBase64String(stringToDeserialize);
            var memorystream = new MemoryStream(bytes);
            var bf = new BinaryFormatter();

            _value = (T)bf.Deserialize(memorystream);
        }

        private void SaveToPrefs()
        {
            var memorystream = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(memorystream, _value);
            var stringToSave = Convert.ToBase64String(memorystream.ToArray());

            PlayerPrefs.SetString(_playerPrefsPath, stringToSave);
        }
    }
}