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
        private IStorageService _storageService;

        [SerializeField] private  ThemeShopFSM _themeShopFSM;

        [Inject]
        public void Construct(IStorageService storageService)
        {
            _storageService = storageService;
        }

        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventTransitionComplete>(SetSelectThemeState);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadMenu>(SetIdleState);
        }

        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventTransitionComplete>(SetSelectThemeState);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadMenu>(SetIdleState);
        }

        private void Start()
        {
            _storageService.Load<LevelData>(Const.LEVLES_KEY + Const.THEME_LEVLE_NAME, BuildLevel);
        }

        public void BuildLevel(LevelData data) => _levelBuilder.BuildLevel(data);

        private void SetSelectThemeState(EventTransitionComplete ctx)
        {
            _themeShopFSM.SetSelectThemeState();
        }

        private void SetIdleState(EventInputLoadMenu ctx)
        {
            _themeShopFSM.SetIdleState();
        }
    }
}
