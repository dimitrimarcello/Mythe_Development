using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_delete : MonoBehaviour {

    Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        rigidbody2D.AddForce(new Vector2(5, 0));
    }
}
