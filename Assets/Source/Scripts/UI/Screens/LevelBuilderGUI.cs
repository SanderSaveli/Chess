using OFG.ChessPeak.LevelBuild;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OFG.ChessPeak
{
    public class LevelBuilderGUI : MonoBehaviour
    {
        [SerializeField] private FieldCreator _filedCreator;
        [SerializeField] private Slider _filedSizeSlider;

        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            _filedSizeSlider.onValueChanged.AddListener(FieldSizeValueChanged);
        }

        private void OnDisable()
        {
            _filedSizeSlider.onValueChanged.RemoveListener(FieldSizeValueChanged);
        }

        public void FieldSizeValueChanged(float fieldSize)
        {
            int roundedFieldSize = Mathf.RoundToInt(fieldSize); 
            Vector2Int sizeVector = new Vector2Int(roundedFieldSize, roundedFieldSize);
            _filedCreator.ChangeFieldSize(sizeVector);
        }

        public void LoadMainMenu()
        {
            _sceneLoader.LoadScene(SceneNames.MainMenu);
        }
    }
}
