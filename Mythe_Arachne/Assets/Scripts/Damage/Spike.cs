using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    private Collider2D collider;
    private PlayerHealth playerHealth;


    private void Start()
    {

        collider = GetComponent<Collider2D>();
        playerHealth = FindObjectOfType<PlayerHealth>();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        playerHealth.TakeDamage(999);

    }
}
