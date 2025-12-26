using Zenject;

namespace OFG.ChessPeak
{
    public class AnimationSlider : SliderView
    {
        private IProjectSettings _projectSettings;

        [Inject]
        public void Construct(IProjectSettings projectSettings)
        {
            _projectSettings = projectSettings;
        }

        protected override void HandleValue(float value)
        {
            _projectSettings.ScreenAnimationDuration = value;
        }

        protected override float SetSliderValue()
        {
            return _projectSettings.ScreenAnimationDuration;
        }
    }
}
