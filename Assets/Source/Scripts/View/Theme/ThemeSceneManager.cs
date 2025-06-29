using OFG.ChessPeak.LevelBuild;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class ThemeSceneManager : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private FieldCreator _fieldCreator;
        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private  ThemeShopFSM _themeShopFSM;
        [SerializeField] private SystemLevelSO _level;

        private IStorageService _storageService;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(IStorageService storageService, SignalBus signalBus)
        {
            _storageService = storageService;
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventTransitionComplete>(SetSelectThemeState);
            _signalBus.Subscribe<SignalStartLoadScene>(SetIdleState);
        }

        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventTransitionComplete>(SetSelectThemeState);
            _signalBus.Unsubscribe<SignalStartLoadScene>(SetIdleState);
        }

        private void Start()
        {
            //_storageService.Load<LevelData>(Const.LEVLES_KEY + Const.THEME_LEVLE_NAME, BuildLevel);
            BuildLevel(_level.GetData());
        }

        public void BuildLevel(LevelData data) => _levelBuilder.BuildLevel(data);

        private void SetSelectThemeState(EventTransitionComplete ctx)
        {
            _themeShopFSM.SetSelectThemeState();
        }

        private void SetIdleState(SignalStartLoadScene ctx)
        {
            _themeShopFSM.SetIdleState();
        }
    }
}
