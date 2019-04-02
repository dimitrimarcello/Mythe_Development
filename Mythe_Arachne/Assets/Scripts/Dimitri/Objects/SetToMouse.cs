using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToMouse : MonoBehaviour {

	private InteractManager getData;
	public float zDepth;

	void Start()
	{
		getData = GameObject.FindObjectOfType<InteractManager>();
	}

	void Update()
	{
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zDepth;
		Vector3 folowPos = getData.currentCamera.ScreenToWorldPoint(mousePos);
		transform.position = folowPos;
	}
	
}
