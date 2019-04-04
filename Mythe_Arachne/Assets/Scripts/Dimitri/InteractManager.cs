using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour {

	public float dragDistance = 2f;
	public Camera currentCamera;
	public float clickDepth = 0f;
	public LayerMask checkLayer;
	public bool onOf = false;
	private Vector3 mouseStart;

	void Update()
	{
		if(Input.GetMouseButton(0) && onOf){
            mouseStart = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            if (CheckStraightInput() != null)
                CheckStraightInput().OnInteract();
		}
	}

	private IInteractable CheckStraightInput(){
		Vector3 mousePos = currentCamera.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;
		RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector3.forward, 100, checkLayer);
        if (ray){
			return ray.collider.gameObject.GetComponent<IInteractable>() ?? null;
		}
		return null;
	}

	private IInteractable DragingLine(Vector3 _mouseStart){
        Vector3 mousePos = currentCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 dir = mousePos - mouseStart;
		float dist = Vector2.Distance(mouseStart, mousePos);
		dist = Mathf.Clamp(dist, 1, dragDistance);
		RaycastHit2D ray = Physics2D.Raycast(mouseStart, dir, dist, checkLayer);
		Debug.DrawRay(mouseStart, dir, Color.red, 1f);
		if(ray){
        	return ray.collider.gameObject.GetComponent<IInteractable>() ?? null;
		}
		return null;
	}

}
