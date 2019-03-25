using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWeaveSlice : MonoBehaviour {
	public WeaveSpawner list;
    public delegate void OnHit();
    public static event OnHit onHit;
    public delegate void OnFail();
    public static event OnFail onFail;

	void OnCollisionEnter2D(Collision2D other)
	{
		WeaveSlice currentHit = other.collider.GetComponent<WeaveSlice>();
		if(currentHit != null && currentHit.disabled){
		Debug.Log("oof");
			onHit();
			Destroy(other.gameObject);
			list.CleanList();
		}
		else
		{
			onFail();
            Destroy(other.gameObject);
            list.CleanList();
		}
	}

}
