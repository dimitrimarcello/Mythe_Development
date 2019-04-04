using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed = 1;

    public Vector3 pos_1;
    public Vector3 pos_2;
    private bool goingTo_1 = false;

    private Transform ts;

    // Use this for initialization
    void Start () {
        ts = transform;

        LineRenderer lr = GetComponent<LineRenderer>();
        pos_1 = lr.GetPosition(0);
        pos_2 = lr.GetPosition(1);
        lr.enabled = false;
        
        ts.position = pos_1;

        ts.position += Vector3.up * 5;
    }

    // Update is called once per frame
    public void UpdateMovement () {
        if (goingTo_1) { Move(-1); } else
        if (!goingTo_1) { Move(1); }
        if (goingTo_1) { GetComponentInChildren<SpriteRenderer>().flipX = false; } else GetComponentInChildren<SpriteRenderer>().flipX = true;
    }

    public float direction;
    void Move(int _direction)
    {
        direction = _direction;
        // set new pos
        ts.position += new Vector3(speed * _direction * Time.deltaTime, 0 , 0);
        // look at the variables 
        if (ts.position.x <= pos_1.x) { goingTo_1 = false; }
        else if (ts.position.x >= pos_2.x) { goingTo_1 = true; }
    }

    public bool UnderMe(float range)
    {
        Vector3 pl = GameObject.FindGameObjectWithTag("Player").transform.position;
        if ( pl.x > pos_1.x && pl.x < pos_2.x && pl.y < ts.position.y && pl.y >= ts.position.y - range)
        {
            return true;
        }
        return false;
    }
}
