using IUP.Toolkit;
using UDK.Audio;
using UnityEngine;

namespace OFG.Chess
{
    public class GameAudioPlayer : MonoBehaviour
    {
        private IMusicPlayer musicPlayer;
        private ISoundPlayer soundPlayer;
        private void Awake()
        {
            musicPlayer = AudioControllerWrapper.instance.musicPlayer;
            soundPlayer = AudioControllerWrapper.instance.soundPlayer;
            //musicPlayer.PlayMusicClip("MainTheme");
            SubscribeOnEvents();
        }

        private void OnDestroy() => UnsubscribeFromEvents();

        private void SubscribeOnEvents()
        {
            EventBus.RegisterCallback<EventWinning>(PlayWinAudio);
            EventBus.RegisterCallback<EventLosing>(PlayLoseAudio);
            EventBus.RegisterCallback<EventFigureSelected>(PlaySelectAudio);
            EventBus.RegisterCallback<EventFigureMoved>(PlayFigureMoveSound);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.UnregisterCallback<EventWinning>(PlayWinAudio);
            EventBus.UnregisterCallback<EventLosing>(PlayLoseAudio);
            EventBus.UnregisterCallback<EventFigureSelected>(PlaySelectAudio);
            EventBus.UnregisterCallback<EventFigureMoved>(PlayFigureMoveSound);
        }

        private void PlayWinAudio(EventWinning ctx)
        {
            soundPlayer.PlaySound2D("Win");
        }

        private void PlayLoseAudio(EventLosing ctx)
        {
            soundPlayer.PlaySound2D("Lose");
        }
        private void PlaySelectAudio(EventFigureSelected ctx)
        {
            soundPlayer.PlaySound2D("FigureSelect");
        }

        private void PlayFigureMoveSound(EventFigureMoved ctx)
        {
            soundPlayer.PlaySound2D("FigurePlace");
        }
    }
}
