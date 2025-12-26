using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OFG.ChessPeak
{
    public class AudioSlider : SliderView
    {
        [SerializeField] private SliderType _sliderType;
        private IProjectSettings _projectSettings;

        [Inject]
        public void Construct(IProjectSettings projectSettings)
        {
            _projectSettings = projectSettings;
        }

        protected override void HandleValue(float value)
        {
            if(_sliderType == SliderType.Sound)
            {
                _projectSettings.SoundVolume = value;
            }
            else
            {
                _projectSettings.MusicVolume = value;
            }
        }

        protected override float SetSliderValue()
        {
            return _sliderType == SliderType.Music? _projectSettings.MusicVolume : _projectSettings.SoundVolume;
        }
    }
}
