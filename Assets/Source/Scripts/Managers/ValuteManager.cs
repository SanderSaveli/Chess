using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class ValuteManager : MonoBehaviour, IValuteManager
    {
        public int CurrentValute => _currentValue.Value;

        private IntPrefsValue _currentValue;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _currentValue = new IntPrefsValue(Const.VALUTE_KEY, 0);
            _signalBus.Fire(new SignalValuteChange(_currentValue.Value, _currentValue.Value));
        }
        public void AddValute(int valute)
        {
            _currentValue.Value = _currentValue.Value + valute;

            _signalBus.Fire(new SignalValuteChange(_currentValue.Value, valute));
        }

        public bool TrySpendValute(int cost)
        {
            if (_currentValue.Value >= cost)
            {
                _currentValue.Value = _currentValue.Value -  cost;
                _signalBus.Fire(new SignalValuteChange(_currentValue.Value, cost*-1));
                return true;
            }
            return false;
        }
    }
}
