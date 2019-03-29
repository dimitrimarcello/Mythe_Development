using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atachable : MonoBehaviour, IHitable {

	public GameObject anchorTexture;
	private List<GameObject> drawnAnchors = new List<GameObject>();
	public int limitAnchors = 1;

	public void OnHit(Vector3 pointOfHit){
		drawnAnchors.Insert(0,Instantiate(anchorTexture, pointOfHit, transform.rotation));
		if (drawnAnchors.Count > limitAnchors)
		{
			Destroy(drawnAnchors[drawnAnchors.Count-1]);
			drawnAnchors.RemoveAt(drawnAnchors.Count-1);
		}
	}

}
