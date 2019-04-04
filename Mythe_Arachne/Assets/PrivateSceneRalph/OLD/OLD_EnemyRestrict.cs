using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_EnemyRestrict : MonoBehaviour {

    private OLD_EnemyMove enemyMove;

	// Use this for initialization
	void Start () {

        enemyMove = GetComponentInParent<OLD_EnemyMove>();

	}
	
	// Update is called once per frame
	public void UpdateRestrict() {
        
        CastRays(enemyMove.speed);

	}

    void CastRays(int direction)
    {
        Vector3 pos = new Vector3();
        Vector3 dir = new Vector3();
        // downRay
        pos = transform.position;
        pos.x = direction + transform.position.x;
        dir = Vector3.down;
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, 0.5f);
            //Debug.DrawRay(pos, dir);
        if (!hit)
        {
            enemyMove.ChangeDirection(direction * -1);
            print("switch");
            return;
        }
        // RightLeftRay
        dir = Vector3.right * direction;
        pos.y += 1;
        hit = Physics2D.Raycast(pos, dir, 0.5f);
            //Debug.DrawRay(pos, dir);
        if (hit)
        {
            if(hit.collider.gameObject.layer == 9)
            {
                enemyMove.ChangeDirection(direction * -1);
                print("wall");
            }
        }
    }
}
