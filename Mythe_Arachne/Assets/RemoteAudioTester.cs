using UnityEngine;
using WiiU = UnityEngine.WiiU;

class RemoteAudioTester : MonoBehaviour
{
    public WiiU.AudioOutput m_OutputDevice = WiiU.AudioOutput.Remote0;

    int RemoteIndex
    {
        get
        {
            return m_OutputDevice == WiiU.AudioOutput.Remote0 ? 0 :
                m_OutputDevice == WiiU.AudioOutput.Remote1 ? 1 :
                m_OutputDevice == WiiU.AudioOutput.Remote2 ? 2 : 3;
        }
    }
    AudioSource audioSource { get { return GetComponent<AudioSource>(); } }

    public void Start()
    {
        WiiU.AudioSourceOutput.Assign(audioSource, m_OutputDevice);
    }

    public void Update()
    {
        var rem = WiiU.Remote.Access(RemoteIndex);
        var state = rem.state;
        if (state.err == WiiU.RemoteError.None)
        {
            if (state.IsTriggered(WiiU.RemoteButton.A))
                audioSource.Play();
            else if (state.IsTriggered(WiiU.RemoteButton.B))
                audioSource.PlayOneShot(audioSource.clip);
            else if (state.IsTriggered(WiiU.RemoteButton.Up))
                audioSource.PlayDelayed(1.0f);
        }
    }
}
