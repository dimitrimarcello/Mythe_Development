using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaveMachine : MonoBehaviour {

	public float speed = 3f;
	public Vector2 clampMag = new Vector2();

	void Update () {
		if(Input.GetKey(KeyCode.A)){
			transform.Translate(-transform.right * Time.deltaTime * speed);
		}
		else if(Input.GetKey(KeyCode.D)){
            transform.Translate(transform.right * Time.deltaTime * speed);
		}
		transform.position = new Vector2(Mathf.Clamp(transform.position.x, clampMag.x, clampMag.y), transform.position.y);
	}
}
