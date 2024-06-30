using UDK.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.Chess
{
    public class SoundSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        IAudioController player;

        private void Start()
        {
            player = AudioControllerWrapper.instance.audioController;
            slider.value = player.SoundVolume;
        }

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(ChangeMusic);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(ChangeMusic);
        }

        private void ChangeMusic(float volume)
        {
            player.SoundVolume = volume;
        }
    }
}
