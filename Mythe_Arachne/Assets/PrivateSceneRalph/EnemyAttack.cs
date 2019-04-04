using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float range = 8;

    private bool diving;
    private float timer;
    public float prevY = 3;
    private Vector3 direction;
    private Vector3 beginPos;

    private EnemyMovement en_mov;
    private Animator anim;

    // Use this for initialization
    void Start () {
        en_mov = GetComponent<EnemyMovement>();
        ColliderTrigger.IsTriggered += Detect;
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    public void EnemyUpdate () {
        Collider2D coll = GetComponentInChildren<BoxCollider2D>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, coll.bounds.size.y/2 + 0.05f , 0) , Vector3.down, 0.1f);
        if (hit)
        {
            if(hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Player")
            {
                bool player = false;
                if (hit.collider.gameObject.tag == "Player") { player = true; }
                HitPlayer(player);
            }
        }
        if (diving)
        {
            if (timer <= 0)
            {
                anim.SetBool("Spotted", false);
                transform.position += direction * (Time.deltaTime / 2);
                if (transform.position.y <= prevY - range || transform.position.x < en_mov.pos_1.x - 2 || transform.position.x > en_mov.pos_2.x + 2) { diving = false; anim.SetBool("Dive", false); }
            }
            else timer -= Time.deltaTime;
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

    private void HitPlayer(bool IsPlayer)
    {
        diving = false;
        anim.SetBool("Dive", false);
        if (IsPlayer) 
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().TakeDamage();
        }
    }

    private void Detect(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Ground" || other.collider.gameObject.tag == "Player")
        {
            bool player = false;
            if (other.collider.gameObject.tag == "Player") { player = true; }
            HitPlayer(player);
        }
    }

    public void Dive()
    {
        direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        float multiplier = 10 / Mathf.Sqrt((direction.x * direction.x) + (direction.y * direction.y));
        direction = new Vector3(direction.x * multiplier, direction.y * multiplier, 0);
        diving = true;
        timer = 1;
        // anim
        anim.SetBool("Spotted", true); 
        anim.SetBool("Dive", true);
        if (direction.x > 0) GetComponentInChildren<SpriteRenderer>().flipX = true; else GetComponentInChildren<SpriteRenderer>().flipX = false;
    }

    public bool IsDiving()
    {
        if(transform.position.y < prevY) { return true; }
        return diving;
    }
}
