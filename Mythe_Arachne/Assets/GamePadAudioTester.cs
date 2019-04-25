using UnityEngine;
using WiiU = UnityEngine.WiiU;

class GamePadAudioTester : MonoBehaviour
{
    AudioSource audioSource { get { return GetComponent<AudioSource>(); } }

    public void Start()
    {
        WiiU.AudioSourceOutput.Assign(audioSource, WiiU.AudioOutput.GamePad);
    }

    public void Update()
    {
        var gamepad = WiiU.GamePad.access;
        var state = gamepad.state;

        if (state.gamePadErr == WiiU.GamePadError.None)
        {
            if (state.IsTriggered(WiiU.GamePadButton.A))
                audioSource.Play();
            else if (state.IsTriggered(WiiU.GamePadButton.B))
                audioSource.PlayOneShot(audioSource.clip);
            else if (state.IsTriggered(WiiU.GamePadButton.Up))
                audioSource.PlayDelayed(1.0f);
        }
    }

    public void OnGUI()
    {
        GUI.Box(new Rect(50, 50, 804, 200), "This example demonstrates audio playback on GamePad and Wii Remotes.\nNote that audio format must be compressed to GCADPCM.\n\n\nOn the device that you want to hear the sound through, press\n[A] to play AudioSource,\n[B] to play OneShot and\n[Up] to PlayDelayed by 1 second");
    }
}
