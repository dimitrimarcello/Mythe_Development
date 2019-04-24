using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public delegate void _PlayerHealth();
    public static event _PlayerHealth Death;

    public Slider slider;

    [HideInInspector]
    public int health;

    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private float noDamageSeconds;

    private bool canTakeDamage = true;

    private void Start()
    {
        health = maxHealth;

        slider.minValue = 0;
        slider.maxValue = maxHealth;
    }

    public void TakeDamage(int damage = 1)
    {
        if (health <= 0) return;
        if (!canTakeDamage) return;

        StartCoroutine(NoDamageCooldown());
        health -= damage;

        if (health < 0) health = 0;

        CheckHealth();

        slider.value = health;
    }

    public void Heal()
    {
        health = maxHealth;
        slider.value = health;
    }

    private void CheckHealth()
    {
        if (health > 0) return;
        Death();
    }

    private IEnumerator NoDamageCooldown()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(noDamageSeconds);

        canTakeDamage = true;
    }


}
