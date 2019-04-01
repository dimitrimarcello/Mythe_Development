using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

        if (!button.interactable)
        text.color = Color.gray;

    }

    void Start()
    {

    }

    public void OnPointerUp()
    {
        if (!button.interactable) return;
        text.color = Color.white;
    }

    public void OnPointerDown()
    {
        if (!button.interactable) return;
        text.color = Color.yellow;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
