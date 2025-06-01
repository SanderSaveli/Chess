namespace OFG.ChessPeak
{
    public abstract class PrefsOrDefaultValue<T>
    {
        public T Value { get => _value; set => SetValue(value); }
        protected T _value;
        protected string _key;

        protected PrefsOrDefaultValue(string key, T defaultValue)
        {
            _key = key;
        }

        protected abstract T SetValue(T value);
    }
}
