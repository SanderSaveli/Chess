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
            soundPlayer.PlaySound2D("Win");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            soundPlayer.PlaySound2D("FigureTaken");
        }

    }
}
