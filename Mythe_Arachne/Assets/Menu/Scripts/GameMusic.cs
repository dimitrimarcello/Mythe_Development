using UnityEngine;

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

        foreach (GameObject obj in GetDontDestroyOnLoadObjects())
        {
            if (obj.GetComponent<GameMusic>() != null) return;
        }

        gameObject.GetComponent<AudioSource>().Play();

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
