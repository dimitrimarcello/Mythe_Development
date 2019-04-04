﻿using UnityEngine;
using UnityEngine.UI;

public class SwingAnimation : MonoBehaviour {

    private Image image;

    [SerializeField]
    private float speed;

    private float movement;

    [SerializeField]
    private float maxRotation;

    [SerializeField]
    private float offset;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
    }
	
	void FixedUpdate () {

        // Creates the swing animation in the menu's
        transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed) + offset);

	}
}
