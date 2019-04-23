using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    private Collider2D collider;
    private PlayerHealth playerHealth;

    bool hurt;


    private void Start()
    {

        collider = GetComponent<Collider2D>();
        playerHealth = FindObjectOfType<PlayerHealth>();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        if (hurt) return; // Als speler al een keer geraakt is called hij niet nog is, aangezien spike instant death is (dus onnodig)

        hurt = true;

        playerHealth.TakeDamage(999);

    }
}
