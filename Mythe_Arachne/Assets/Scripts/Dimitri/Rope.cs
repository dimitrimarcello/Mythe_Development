using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RopeType
{
	Climb, Bounce
}
public class Rope : ScriptableObject {

	public RopeType ropeType;
	public List<GameObject> ropePieces = new List<GameObject>();
	public int ropePiecesAmount = 5;

	public void ActivateMovement(){
		foreach (GameObject piece in ropePieces)
		{
			if(piece != ropePieces[ropePieces.Count-1]){
				piece.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			}
		}
	}

	public void DestroyRope(){
		foreach (GameObject piece in ropePieces)
		{
			Destroy(piece);
		}
	}

}
