using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class ThemeMaterialHandler : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private MaterialType _materialType;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<SignalThemeChanged>(HandleTheme);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<SignalThemeChanged>(HandleTheme);
        }

        private void HandleTheme(SignalThemeChanged ctx)
        {
            switch (_materialType)
            {
                case MaterialType.King:
                    break;
                case MaterialType.Figure:
                    break;
                default:
                    break;
            }
        }
    }
}
