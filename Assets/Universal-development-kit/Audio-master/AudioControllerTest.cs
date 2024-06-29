using UDK.Audio;
using UnityEngine;

public class AudioControllerTest : MonoBehaviour
{
    IMusicPlayer musicPlayer;
    ISoundPlayer soundPlayer;
    IAudioController controller;

    private void Start()
    {
        AudioControllerWrapper controllerWrapper = AudioControllerWrapper.instance;
        musicPlayer = controllerWrapper.musicPlayer;
        soundPlayer = controllerWrapper.soundPlayer;
        controller = controllerWrapper.audioController;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            musicPlayer.PlayMusicClip("Music1");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            musicPlayer.PlayMusicClip("Music2");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            soundPlayer.PlayAudioClip2D("Test1");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            soundPlayer.PlayAudioClip2D("Test2");
        }
    }
}
