using System;
using UnityEngine;
using UnityEngine.Audio;
using WiiU = UnityEngine.WiiU;

public class SoundManager : MonoBehaviour {

    [SerializeField]
    private AudioMixerGroup audioMixerGroup;

    [SerializeField]
    private Sound[] sounds;

    private static SoundManager instance;

	// Use this for initialization
	void Awake () {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();

            WiiU.AudioSourceOutput.Assign(audioSource, WiiU.AudioOutput.GamePad);

            s.source = audioSource;

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixerGroup;
        }
	}
	
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }
}
