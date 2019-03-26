using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour {

    private Transform target;

	// Use this for initialization
	void Start () {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, target.position, Time.deltaTime, 360.0f));

	}
}
