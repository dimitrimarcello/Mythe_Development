using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RopeType
{
	None, Climb, Bounce
}
public class Rope : ScriptableObject {

	public RopeType ropeType;
	public List<GameObject> ropePieces = new List<GameObject>();
	public int ropePiecesAmount = 5;

	public void ActivateMovement(){
		foreach (GameObject piece in ropePieces)
		{
			if(piece != null)
				piece.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		}

	}
	public void DestroyRope(){
		foreach (GameObject piece in ropePieces)
		{
			if(piece != null)
				Destroy(piece);
		}
	}

	public RopeType GetRopeType(){
		int anchorAmount = 0;
		foreach (GameObject piece in ropePieces){
			RopePiece currentRope = piece.GetComponent<RopePiece>();
			if(currentRope.isAnchor){
				anchorAmount++;
			}
			else if(piece.GetComponent<HingeJoint2D>().connectedBody == null)
			{
				anchorAmount++;
			}
		}
		RopeType returnThis = (RopeType)anchorAmount;
		foreach (GameObject piece in ropePieces)
		{
			piece.GetComponent<RopePiece>().thisType = returnThis;
		}
		Debug.Log(returnThis);
		return returnThis;
	}

}
