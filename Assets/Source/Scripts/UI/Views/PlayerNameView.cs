using TMPro;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class PlayerNameView : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private TMP_Text _text;

        private SignalBus _signalBus;
        private IAccountManager _accountManager;

        [Inject]
        public void Construct(SignalBus signalBus, IAccountManager accountManager)
        {
            _signalBus = signalBus;
            _accountManager = accountManager;
        }

        private void OnEnable()
        {
            _text.text = _accountManager.Name;
            _signalBus.Subscribe<SignalPlayerAccountUpdated>(HandleAccountUpdate);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<SignalPlayerAccountUpdated>(HandleAccountUpdate);
        }

        private void HandleAccountUpdate(SignalPlayerAccountUpdated ctx)
        {
            _text.text = ctx.PlayerNetworkData.name;
        }
    }
}
