using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class GameMusic : MonoBehaviour {

    private static GameMusic instance;

	// Use this for initialization
	void Start () {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        AudioSource audioSource = gameObject.GetComponent<AudioSource>();

        WiiU.AudioSourceOutput.Assign(audioSource, WiiU.AudioOutput.GamePad);

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
