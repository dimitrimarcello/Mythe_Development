//////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////
/////// This script has been build to be the movement for the game, cool right. ;) ///////
//////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////// -Sjors

/*
____________________________
|__________________________|
||    * - Changes - *     ||
||========================||
|| ~25-2-2019 By Sjors ~  ||
|| + Basic movement       ||
||________________________||

Please note any changes, Thank you! ^-^*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField]float Speed = 1f, /*ForceJump = 5f,*/ CastLenght = 1.1f;
    PlayerInput playerInput;
    Rigidbody2D rb;
    int layerMask = ~(1 << 8);
    //bool Jumping = true;

    [ExecuteInEditMode]
    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        Move();
        CheckCasts();
        //Jump();
    }

    void Move () {
        Vector2 movementInput = playerInput.JoystickMove;
        transform.Translate(movementInput.x * (Speed * 10) * Time.deltaTime, 0, 0);
    }

    /*
	void Jump () {
        if (playerInput.AB && !Jumping)
        {
            Jumping = true;
            rb.AddForce((Vector2.up * (ForceJump * 5000)) * Time.deltaTime);
        }
	}
    */

    void CheckCasts()
    {
        RaycastHit2D downWard = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - transform.lossyScale.y / 2), Vector2.down, (CastLenght / 10), layerMask);

        if (downWard.collider == null)
        {
            //Jumping = true;
        }
        else
        {
            //Jumping = false;
        }
    }

}
