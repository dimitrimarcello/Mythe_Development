using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private bool diving;
    private float prevY;
    private Vector3 direction;
    private Vector3 beginPos;

    private EnemyMovement en_mov;

    // Use this for initialization
    void Start () {
        en_mov = GetComponent<EnemyMovement>();
        prevY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        Collider2D coll = GetComponentInChildren<BoxCollider2D>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, coll.bounds.size.y/2 + 0.05f , 0) , Vector3.down, 0.1f);
        if (hit)
        {
            if(hit.collider.gameObject.tag == "Ground")
            {
                diving = false;
            }
            if(hit.collider.gameObject.tag == "Player")
            {
                diving = false;
            }
        }
        if (diving)
        {
            transform.position += (direction - beginPos) * (Time.deltaTime / 2);
        }
        if (!diving)
        {
            if(transform.position.y != prevY)
            {
                transform.position += Vector3.up * Time.deltaTime;
                if (transform.position.y > prevY) { transform.position = new Vector3(transform.position.x, prevY, transform.position.z); }
            }
        }
    }

    public void Dive()
    {
        direction = GameObject.FindGameObjectWithTag("Player").transform.position;
        beginPos = transform.position;
        diving = true;
        // anim
    }

    public bool IsDiving()
    {
        if (transform.position.y >= prevY) { return false; }
        return true;
    }
}
