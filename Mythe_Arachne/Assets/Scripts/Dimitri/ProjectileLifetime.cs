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

}
