using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 12) return;

        SceneManager.LoadScene(2);
    }
}
