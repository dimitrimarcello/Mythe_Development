//////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////
/////// This script has been build to be the movement for the game, cool right. ;) ///////
//////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////// -Sjors

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 2)] float speed = 1f, /*ForceJump = 5f,*/ castLenght = 1.4f;
    PlayerInput playerInput;
    Rigidbody2D rb;
    Collider2D col;
    RaycastHit2D sideL, sideR, lastUsed;
    SpriteRenderer sides;
    public LayerMask layerMask, swingMask; //Give values with what the raycasts can interract(in this case excluding player layer)

    public bool Grounded { get; private set; }
    [SerializeField] bool Swing;

    [ExecuteInEditMode]
    //Get all the required components, and lock the rigidbody's rotations
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        playerInput = GetComponent<PlayerInput>();
        sides = GetComponent<SpriteRenderer>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    //Make sure all functions run
    private void FixedUpdate()
    {
        Move();
        CheckCasts();
    }

    //Movement Script with inputs and joysticks, depending on colliders
    void Move()
    {

        Vector2 movementInput = playerInput.JoystickMove;

        if (/*sideL.collider == null && */movementInput.x < 0 || /*sideR.collider == null && */movementInput.x > 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            transform.Translate(new Vector3(movementInput.x * speed * Time.timeScale, 0, 0));
        }

        if(movementInput.x < 0)
        {
            sides.flipX = true;
        }
        else if(movementInput.x > 0)
        {
            sides.flipX = false;
        }


        if (Swing)
        {
            if (!col.enabled)
            {
                try
                {
                    transform.position = lastUsed.transform.position;
                }
                catch
                {
                    StopHanging();
                }
            }
            if (playerInput.ZLZR == true)
            {
                StopHanging();
            }

            if (sideL.collider != null && sideL.collider.GetComponent<RopePiece>().thisType == RopeType.Climb)
            {
                RopeAction(sideL);
            }
            if (sideR.collider != null && sideR.collider.GetComponent<RopePiece>().thisType == RopeType.Climb)
            {
                RopeAction(sideR);
            }
        }
    }

    //Rope haninging
    void RopeAction(RaycastHit2D target)
    {
        //bool ifhanging
        if (col.enabled)
        {
            target.collider.enabled = false;
            col.enabled = false;
            rb.isKinematic = true;
            gameObject.transform.parent = target.transform;
            lastUsed = target;
        }
    }

    //cancel hanging
    void StopHanging()
    {
        col.enabled = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.isKinematic = false;
        gameObject.transform.parent = null;
        StartCoroutine(toggleColliderBack());
    }

    IEnumerator toggleColliderBack()
    {
        yield return new WaitForSeconds(1);
        try
        {
            lastUsed.collider.enabled = true;
        }
        catch
        {
            Debug.LogWarning("An error has occured, last used other collider has not been found.");
        }

    }

    //Check all the raycasts
    void CheckCasts()
    {

        RaycastHit2D downWard = Physics2D.Raycast(transform.position, Vector2.down, castLenght, layerMask);
        if (downWard.collider != null)
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }

        sideL = Physics2D.Raycast(transform.position, Vector2.left, (castLenght * col.bounds.size.x / 2), swingMask);
        sideR = Physics2D.Raycast(transform.position, Vector2.right, (castLenght * col.bounds.size.x / 2), swingMask);
    }
}
