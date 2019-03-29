using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TempPlayer : MonoBehaviour, IInteractable {

	public GameObject projectile;
	public Camera getPoint;
	public float throwForce = 10f;
	public float drawDistance = 3f;
	private bool isBusy = false;

	public void OnInteract(Vector3 mousePos){
		if(!isBusy)
		StartCoroutine(StartSchooting());
	}

	public void OnHit(){
		
	}

	private IEnumerator StartSchooting(){
		isBusy = true;
		RaycastHit2D shootDir = Physics2D.Raycast(transform.position, transform.position);
		Vector2 mousePos = transform.position;
		while(Input.GetMouseButton(0)){
			mousePos = getPoint.ScreenToWorldPoint(Input.mousePosition);
            shootDir = Physics2D.Raycast(transform.position, mousePos, drawDistance);
			yield return new WaitForFixedUpdate();
		}
		GameObject tempProjectile = Instantiate(projectile,transform.position, transform.rotation);
        Vector2 dir = mousePos - (Vector2)transform.position;
        float angle = -Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;// Radials to degrees
        tempProjectile.transform.eulerAngles = new Vector3(0, 0, angle + 90);// Set the angle that was converted to degrees as Z axis
		float dist = Vector2.Distance(transform.position, mousePos);
		dist = Mathf.Clamp(dist, 0, drawDistance);
		Debug.Log(dist);
		tempProjectile.GetComponent<Rigidbody2D>().AddForce(dir * dist * throwForce, ForceMode2D.Impulse);
		isBusy = false;
	}

}
