using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUI : MonoBehaviour {

	public Text sucsesful, failure;
	private int pos, negative;

	void Start()
	{
		CheckWeaveSlice.onHit += AddPositive;
		CheckWeaveSlice.onFail += AddNegative;
	}

	public void AddPositive(){
		pos ++;
		sucsesful.text = "" + pos;
	}

	public void AddNegative(){
		negative ++;
		failure.text = "" + negative;
	}

}
