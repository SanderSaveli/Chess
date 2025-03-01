using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class MenuLoader : MonoBehaviour
    {

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            _signalBus.Fire(new SignalInputLoadScene(SceneNames.MainMenu));
        }
    }
}
