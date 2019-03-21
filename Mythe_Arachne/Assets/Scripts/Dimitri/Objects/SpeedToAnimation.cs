using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedToAnimation : MonoBehaviour {

	public Animator setValues;
	private Rigidbody2D getValues;

	void Awake()
	{
		getValues = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		setValues.SetFloat("VelX", Input.GetAxis("Horizontal"));
        setValues.SetFloat("VelY", getValues.velocity.y);
	}

}
