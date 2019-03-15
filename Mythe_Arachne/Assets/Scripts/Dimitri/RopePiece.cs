using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePiece : MonoBehaviour, IInteractable {

	public GameObject anchorVisuals;
	public RopeType thisType;
	public bool isAnchor = false;
	private bool done = false;
	private bool broken = false;
	public float bounceForce = 5f;
	private Camera point;

	void Start()
	{
		if(GetComponent<HingeJoint2D>().connectedBody == null){
			isAnchor = true;
		}
		point = GameObject.Find("GampadCam").GetComponent<Camera>();
	}

	public void OnInteract(Vector3 mousePos){
		if(GetComponent<HingeJoint2D>().attachedRigidbody != null){
			if(isAnchor || thisType == RopeType.Climb){
				broken = true;
				GetComponent<HingeJoint2D>().enabled = false;
				GetComponent<BoxCollider2D>().enabled = false;
			}
			else
			{
				StartCoroutine(StartDraging());
			}
		}
	}

	public void OnHit(){
		
	}

	private IEnumerator StartDraging()
	{
		Vector3 dir = new Vector3(0,0,0);
        Rigidbody2D thisObj = GetComponent<Rigidbody2D>();
		while(Input.GetMouseButton(0)){
			dir = point.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			thisObj.AddForce(Vector3.up * bounceForce, ForceMode2D.Impulse);
            thisObj.velocity = new Vector2(Mathf.Clamp(thisObj.velocity.y, -bounceForce, bounceForce), Mathf.Clamp(thisObj.velocity.y, -bounceForce, bounceForce));
			yield return null;
		}
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
