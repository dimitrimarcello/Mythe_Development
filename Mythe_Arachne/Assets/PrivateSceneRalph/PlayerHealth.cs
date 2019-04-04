using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public delegate void _PlayerHealth();
    public static event _PlayerHealth Death;

    public Slider slider;
    public int health = 3;
    private float timer;

    private void FixedUpdate()
    {
        if (timer > 0) { timer -= Time.deltaTime; }
    }

    public void TakeDamage(int damage = 1)
    {
        if (timer <= 0)
        {
        CheckHealth(); 
            health -= damage;
            slider.value = health;
            timer += 1;
        }
    }

    public void Heal(int healing = 2)
    {
        health += healing;
        slider.value = health;
    }

    private void CheckHealth()
    {
        if (health >= 0)
        {
            Death();
        }
    }
}
