using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFolow : MonoBehaviour {

	private InteractManager getData;
	public float zDepth;

	void Start()
	{
		getData = GameObject.FindObjectOfType<InteractManager>();
	}

	void Update()
	{
		Vector3 folowPos = getData.currentCamera.ScreenToWorldPoint(Input.mousePosition);
		folowPos.z = zDepth;
		transform.position = folowPos;
	}
	
}
