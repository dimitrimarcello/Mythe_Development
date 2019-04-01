using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwingAnimation : MonoBehaviour {

    private Image image;

    [SerializeField]
    private float speed;

    private float movement;

    [SerializeField]
    private float maxRotation;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
    }
	
	void FixedUpdate () {

        transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));

	}
}
