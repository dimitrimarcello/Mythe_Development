using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public delegate void _PlayerHealth();
    public static event _PlayerHealth Death;

    public Slider slider;

    private int health = 3;
    private float timer;

    private void FixedUpdate()
    {
        if (timer > 0) { timer -= Time.deltaTime; }
    }

    public void TakeDamage()
    {
        if (timer <= 0)
        {
            health--;
            slider.value = health;
            timer += 1;
        }
    }

    private void CheckHealth()
    {
        if (health >= 0)
        {
            Death();
        }
    }
}
