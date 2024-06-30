using UDK.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.Chess
{
    public class MusicSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        IAudioController player;

        private void Start()
        {
            player = AudioControllerWrapper.instance.audioController;
            slider.value = player.MusicVolume;
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
            player.MusicVolume = volume;
        }
    }
}
