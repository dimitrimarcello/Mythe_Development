using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorManager : MonoBehaviour, IInteractable {

	private DrawManager drawOn;
	private InteractManager interactOn;

	void Start()
	{
		drawOn = GameObject.FindObjectOfType<DrawManager>().GetComponent<DrawManager>();
        interactOn = GameObject.FindObjectOfType<InteractManager>().GetComponent<InteractManager>();
	}

	public void OnInteract(){
		StartCoroutine(CheckIfSchooting());
	}

	public void OnHit(){
		
	}
	
	public IEnumerator CheckIfSchooting(){
		drawOn.onOf = true;
		interactOn.onOf = false;
		while(Input.GetMouseButton(0)){
			yield return new WaitForFixedUpdate();
		}
        drawOn.onOf = false;
        interactOn.onOf = true;
	}

}
