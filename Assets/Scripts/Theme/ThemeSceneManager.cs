using OFG.ChessPeak.LevelBuild;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class ThemeSceneManager : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private FieldCreator _fieldCreator;
        [SerializeField] private LevelBuilder _levelBuilder;
        private IStorageService _storageService;

        [SerializeField] private  ThemeShopFSM _themeShopFSM;
        private string themeLevelKey = "theme_scene";

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
            _storageService = new JsonToStreamingAssetsStorageService();
            _storageService.Load<LevelData>(themeLevelKey, BuildLevel);
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
