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
    [SerializeField] [Range(0, 2)] float speed = 1f, /*ForceJump = 5f,*/ castLenght = 1.1f;
    [SerializeField] string ropeTag = "Rope";
    PlayerInput playerInput;

    Rigidbody2D rb;
    Collider2D col;
    RaycastHit2D sideL, sideR;
    int layerMask = ~(1 << 9); //Give values with what the raycasts can interract(in this case excluding player layer)

    //bool Jumping = true;

    [ExecuteInEditMode]
    //Get all the required components, and lock the rigidbody's rotations
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        playerInput = GetComponent<PlayerInput>();
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
        Vector2 movementInput = playerInput.JoystickMove;
        if (((sideL.collider == null && movementInput.x < 0) || (sideR.collider == null && movementInput.x > 0)))
        {
            transform.Translate(movementInput.x * (speed * 10) * Time.deltaTime, 0, 0);
        }

        if (playerInput.ZLZR == true)
        {
            StopHanging();
        }

        if (sideL.collider != null && sideL.collider.tag == ropeTag)
        {
            RopeAction(sideL);
        }
        if (sideR.collider != null && sideR.collider.tag == ropeTag)
        {
            RopeAction(sideR);
        }
    }

    //Rope haninging
    void RopeAction(RaycastHit2D target)
    {
        col.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = target.transform.position;
        //gameObject.transform.parent = target.transform;
        //transform.position = target.point; //unused
    }

    //cancel hanging
    void StopHanging()
    {
        col.enabled = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.transform.parent = null;
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
        RaycastHit2D downWard = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - transform.lossyScale.y / 2), Vector2.down, (castLenght / 10), layerMask);
        if(downWard.collider.tag == ropeTag)
        {
            transform.rotation = Quaternion.Euler(0, downWard.collider.transform.rotation.y + 90, 0);
        }
        /*
        else
        {
            Jumping = false;
        }
        */

        //See where colliders are at the sides by taking the size of the player, and basing it off that with a lenght distance. (math aka magic)
        sideL = Physics2D.Raycast(transform.position, Vector2.left, (castLenght * col.bounds.size.x / 2), layerMask);
        sideR = Physics2D.Raycast(transform.position, Vector2.right, (castLenght * col.bounds.size.x / 2), layerMask);
        Debug.DrawLine(transform.position, transform.position + new Vector3(-((castLenght * col.bounds.size.x / 2)), 0));
        Debug.DrawLine(transform.position, transform.position + new Vector3((castLenght * col.bounds.size.x / 2), 0));
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
        Debug.Log(dist);
        tempProjectile.GetComponent<Rigidbody2D>().AddForce(dir * dist * throwForce, ForceMode2D.Impulse);
        isBusy = false;
    }
    ///End Dimitri code
}
