using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePiece : MonoBehaviour, IInteractable {

	public GameObject anchorVisuals;
	private bool done = false;
	private bool broken = false;

	public void OnInteract(){
		if(GetComponent<HingeJoint2D>().attachedRigidbody != null){
			broken = true;
			GetComponent<HingeJoint2D>().enabled = false;
		}
	}

	public void OnHit(){
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.GetComponent<AnchorManager>() != null && !done && !broken){
			GameObject anchorPoint = Instantiate(anchorVisuals, other.contacts[0].point, other.transform.rotation);
			anchorPoint.GetComponent<HingeJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
			done = true;
		}
	}

}
