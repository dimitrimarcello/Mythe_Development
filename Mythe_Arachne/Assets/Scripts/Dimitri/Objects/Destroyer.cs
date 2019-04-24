using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public int damage = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<PlayerHealth>() != null)
        {
            collision.collider.GetComponent<PlayerHealth>().TakeDamage(collision.collider.GetComponent<PlayerHealth>().health);
        }
    }
}
