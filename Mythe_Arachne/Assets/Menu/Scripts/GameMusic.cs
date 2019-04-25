using UnityEngine;

#if UNITY_WIIU && !UNITY_ENGINE
using WiiU = UnityEngine.WiiU;
#endif

public class GameMusic : MonoBehaviour {

    public static GameMusic instance;

    public AudioSource audioSource;

	// Use this for initialization
	void Start () {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.volume = PlayerPrefs.GetFloat("settings_music");


#if UNITY_WIIU && !UNITY_ENGINE
        WiiU.AudioSourceOutput.Assign(audioSource, WiiU.AudioOutput.GamePad);
        WiiU.AudioSourceOutput.Assign(audioSource, WiiU.AudioOutput.TV);
#endif

        audioSource.Play();

        DontDestroyOnLoad(gameObject);

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject[] GetDontDestroyOnLoadObjects()
    {
        GameObject temp = null;
        try
        {
            temp = new GameObject();
            DontDestroyOnLoad(temp);
            UnityEngine.SceneManagement.Scene dontDestroyOnLoad = temp.scene;
            DestroyImmediate(temp);
            temp = null;

            return dontDestroyOnLoad.GetRootGameObjects();
        }
        finally
        {
            if (temp != null)
                Object.DestroyImmediate(temp);
        }
    }
}
