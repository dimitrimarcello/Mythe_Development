using UnityEngine;
using UnityEngine.UI;

public class ButtonCollider : MonoBehaviour {

    private Image image;
    private Button button;

    [SerializeField]
    private Text text;

    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponentInParent<Button>();

        Color color = image.color;

        color.a = 0f;

        image.color = color;

        // Makes text of button gray when it is not interactable
        if (!button.interactable)
        text.color = Color.gray;

    }

    // Makes text of button white when button is released
    public void OnPointerUp()
    {
        if (!button.interactable) return;
        text.color = Color.white;
    }

    // Makes text of button yellow when button is pressed
    public void OnPointerDown()
    {
        if (!button.interactable) return;
        text.color = Color.yellow;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
