using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour {

    private float speed = 1;
    private Transform target;
    public Rigidbody2D rb2d;
    

    public void Shoot(Transform _ts)
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = _ts.position;
        Vector3 dot = target.position - transform.position;
        rb2d.AddForce(dot * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.OnHit();
                Destroy(gameObject);
            }
        }
    }

}
