using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour, IInteractable {

    public delegate void EnemyCollider(Collision2D other);
    public static event EnemyCollider IsTriggered;

    public delegate void GotHit();
    public static event GotHit Hit;

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("collision detected");
        IsTriggered(other);
    }

    public void OnInteract()
    {
        return;
    }

    public void OnHit()
    {
        Hit();
    }

}
