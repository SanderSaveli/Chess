using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace OFG.ChessPeak
{
    public class BootstarapInstaller : MonoInstaller
    {
        [Header(H.Components)]
        [SerializeField] private ThemeManager _themeManager;
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<ThemeManager>().FromInstance(_themeManager).AsSingle().NonLazy();

            #region Signals
            Container.DeclareSignal<SignalInputLoadScene>();
            #endregion
        }

        private void Start()
        {
#if UNITY_EDITOR
            SceneManager.LoadScene(0);
#endif
        }
    }
}
