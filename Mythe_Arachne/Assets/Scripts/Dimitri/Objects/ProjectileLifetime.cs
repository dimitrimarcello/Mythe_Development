using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifetime : MonoBehaviour {

	private Rigidbody2D rg2d;
	private SpriteRenderer alpha;
	private ParticleSystem fadeParticle;
	public float bounceForce = 2;

	void Awake()
	{
        rg2d = GetComponent<Rigidbody2D>();
		alpha = GetComponent<SpriteRenderer>();
		fadeParticle = GetComponent<ParticleSystem>();
	}

	private IEnumerator DestroyThis()
	{
		float temp = 0;
		while(temp < 1){
			alpha.color = Color.Lerp(alpha.color, new Color(1,1,1,0), temp);
			temp += 0.1f;
			yield return new WaitForSeconds(0.1f);
		}
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.GetComponent<IInteractable>() != null){
            other.gameObject.GetComponent<IInteractable>().OnHit();
		}
		Vector2 angle = new Vector2(rg2d.velocity.x, rg2d.velocity.y);
		rg2d.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
		fadeParticle.enableEmission = false;
		StartCoroutine(DestroyThis());
	}

	void Update()
	{
		float angle = -Mathf.Atan2(rg2d.velocity.x, rg2d.velocity.y) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0,0,angle + 90);

	}

}
