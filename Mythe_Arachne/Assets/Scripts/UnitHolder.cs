using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHolder : MonoBehaviour {

    [Range(0, 100), SerializeField]
    private readonly float range;

    [Range(0, 10), SerializeField]
    private readonly float precision; // The lower this number the more precise the HoldInPlace calculation gets

    [SerializeField]
    private Transform player;

    private float distanceGround;

    private bool grounded;

    [SerializeField]
    private readonly float checkDelay;

	// Use this for initialization
	void Start () {
        distanceGround = player.GetComponent<Collider2D>().bounds.extents.y;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = Physics2D.Raycast(transform.position, -Vector2.up, distanceGround + 0.1f);

        FixHolder(wasGrounded);
        

        HoldInPlace();
    }

    void FixHolder(bool wasGrounded)
    {
        if (!(!wasGrounded && grounded)) return;

        StartCoroutine(CheckIfStillGrounded());
    }

    void HoldInPlace()
    {
        if (Vector2.Distance(player.position, transform.position) <= range) return;

        float t = 0;

        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector2 position = transform.position;

        while (true)
        {
            t += precision;

            float x = direction.x * t;
            float y = direction.y * t;

            position += new Vector2(x, y);

            float distance = Vector2.Distance(player.position, position);

            if (distance <= range)
            {
                transform.position = position;
                return;
            }

            position -= new Vector2(x, y);
        }
    }

    private IEnumerator CheckIfStillGrounded()
    {
        yield return new WaitForSeconds(checkDelay);

        while (grounded)
        {

            Vector2 position = new Vector2(transform.position.x, player.transform.position.y);

            transform.position = Vector2.Lerp(transform.position, position, 0.1f);

            if (Vector2.Distance(transform.position, position) < 1f) break;

        }
    }
}
