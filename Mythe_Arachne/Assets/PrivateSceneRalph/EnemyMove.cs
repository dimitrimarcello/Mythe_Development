using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float speedMultiplier = 2;
    public int speed = 1;
    private bool goingRight = true;

    public SpriteRenderer sprRend;


    public void UpdateMove()
    {
        transform.position += Vector3.right * speed * speedMultiplier * Time.deltaTime;
    }

    public void ChangeDirection(int direction)
    {
        // check the variables that have gone through
        if(direction == 1)
        {
            sprRend.flipX = goingRight = true;
            speed = 1;

        }
        else if (direction == -1)
        {
            sprRend.flipX = goingRight = false;
            speed = -1;
        }
    }
}
