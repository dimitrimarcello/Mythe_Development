using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour {

	public Vector2 offset;
	public Vector3 getPos { get{ return new Vector2(transform.position.x + offset.x, transform.position.y + offset.y); } }
	
}
