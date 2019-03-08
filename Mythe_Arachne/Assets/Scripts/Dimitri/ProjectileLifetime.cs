using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifetime : MonoBehaviour {

	public float lifeTime = 2f;

	void Awake()
	{
		StartCoroutine(DestroyThis());
	}

	private IEnumerator DestroyThis()
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.GetComponent<IInteractable>() != null){
            other.gameObject.GetComponent<IInteractable>().OnHit();
		}
	}

}
