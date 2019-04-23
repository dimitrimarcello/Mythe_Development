using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public delegate void _PlayerHealth();
    public static event _PlayerHealth Death;

    public Slider slider;
    public int health = 3;

    [SerializeField]
    private float noDamageSeconds;

    private bool canTakeDamage = true;

    public void TakeDamage(int damage = 1)
    {
        if (health <= 0) return;
        if (!canTakeDamage) return;

        StartCoroutine(NoDamageCooldown());
        CheckHealth();
        health -= damage;

        if (health < 0) health = 0;

        slider.value = health;
    }

    public void Heal(int healing = 2)
    {
        health += healing;
        slider.value = health;
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    private IEnumerator NoDamageCooldown()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(noDamageSeconds);

        canTakeDamage = true;
    }
}
