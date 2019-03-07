using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorManager : MonoBehaviour, IInteractable {

	public DrawManager drawOn;
	public InteractManager interactOn;

	public void OnInteract(){
		StartCoroutine(CheckIfSchooting());
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
