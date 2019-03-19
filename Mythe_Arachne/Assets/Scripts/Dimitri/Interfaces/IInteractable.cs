using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable{
	void OnInteract(Vector3 mousePos);
}
public interface IHitable{
    void OnHit(Vector3 pointOfHit);
}
