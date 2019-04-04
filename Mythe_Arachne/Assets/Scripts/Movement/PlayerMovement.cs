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
public class PlayerMovement : MonoBehaviour, IInteractable
{
    [SerializeField] [Range(0, 2)] float speed = 1f, /*ForceJump = 5f,*/ castLenght = 1.4f;
    PlayerInput playerInput;
    Rigidbody2D rb;
    Collider2D col;
    RaycastHit2D sideL, sideR, lastUsed;
    SpriteRenderer sides;
    public LayerMask layerMask, swingMask; //Give values with what the raycasts can interract(in this case excluding player layer)

    public bool Grounded { get; private set; }

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
        //Jump(); //Jump has been disabled but has been asked to stay in here for whatever reason, sorry!
    }

    //Movement Script with inputs and joysticks, depending on colliders
    void Move()
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

        Vector2 movementInput = playerInput.JoystickMove;

        if (/*sideL.collider == null && */movementInput.x < 0 || /*sideR.collider == null && */movementInput.x > 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            transform.Translate(new Vector3(movementInput.x * speed, 0, 0));
        }

        if(movementInput.x < 0)
        {
            sides.flipX = true;
        }
        else if(movementInput.x > 0)
        {
            sides.flipX = false;
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
        //rb.constraints = RigidbodyConstraints2D.None;
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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


    /* 
    //Jump Function     
	void Jump () {
        if (playerInput.AB && !Jumping)
        {
            Jumping = true;
            rb.AddForce((Vector2.up * (ForceJump * 5000)) * Time.deltaTime);
        }
	}
    */

    //Check all the raycasts
    void CheckCasts()
    {

        RaycastHit2D downWard = Physics2D.Raycast(transform.position, Vector2.down, castLenght, layerMask);
        if (downWard.collider != null)
        {
            //transform.rotation = Quaternion.Euler(0, downWard.collider.transform.rotation.y + 90, 0);
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }


        //See where colliders are at the sides by taking the size of the player, and basing it off that with a lenght distance. (math aka magic)
        sideL = Physics2D.Raycast(transform.position, Vector2.left, (castLenght * col.bounds.size.x / 2), swingMask);
        sideR = Physics2D.Raycast(transform.position, Vector2.right, (castLenght * col.bounds.size.x / 2), swingMask);
        //Debug.DrawLine(transform.position, transform.position + new Vector3(-((castLenght * col.bounds.size.x / 2)), 0));
        //Debug.DrawLine(transform.position, transform.position + new Vector3((castLenght * col.bounds.size.x / 2), 0));
    }

    ///Dimitri code
    public GameObject projectile;
    public Camera getPoint;
    public float throwForce = 10f;
    public float drawDistance = 3f;
    private bool isBusy = false;

    public void OnInteract(Vector3 mousePos)
    {
        if (!isBusy)
            StartCoroutine(StartSchooting());
    }

    private IEnumerator StartSchooting()
    {
        isBusy = true;
        RaycastHit2D shootDir = Physics2D.Raycast(transform.position, transform.position);
        Vector2 mousePos = transform.position;
        while (Input.GetMouseButton(0))
        {
            mousePos = getPoint.ScreenToWorldPoint(Input.mousePosition);
            shootDir = Physics2D.Raycast(transform.position, mousePos, drawDistance);
            yield return new WaitForFixedUpdate();
        }
        GameObject tempProjectile = Instantiate(projectile, transform.position, transform.rotation);
        Vector2 dir = mousePos - (Vector2)transform.position;
        float dist = Vector2.Distance(transform.position, mousePos);
        dist = Mathf.Clamp(dist, 0, drawDistance);
        //Debug.Log(dist);
        tempProjectile.GetComponent<Rigidbody2D>().AddForce(dir * dist * throwForce, ForceMode2D.Impulse);
        isBusy = false;
    }
    ///End Dimitri code
}
