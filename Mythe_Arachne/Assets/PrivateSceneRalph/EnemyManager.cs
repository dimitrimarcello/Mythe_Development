using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IInteractable {

    public float radius;

    private float stunTimer;
    private bool standstill;

    private Transform playerTS;
    private EnemyMove enemyMove;
    private EnemyRestrict enemyRestrict;
    private EnemyShoot enemyShoot;

	// Use this for initialization
	void Start () {

        playerTS = GameObject.FindGameObjectWithTag("Player").transform;
        enemyMove = GetComponent<EnemyMove>();
        enemyRestrict = GetComponentInChildren<EnemyRestrict>();
        enemyShoot = GetComponent<EnemyShoot>();

    }
	
	// Update is called once per frame
	void Update () {

        // check if your stunned yes or no
        if (stunTimer <= 0)
        {
            // check if you can or cant see the player or not
            if (!CheckForPlayer())
            {
                // Update Move and Restrictions
                enemyMove.UpdateMove();
                enemyRestrict.UpdateRestrict();
            }
        }
        else stunTimer -= Time.deltaTime;

	}

    private bool CheckForPlayer()
    {
        // variables
        Vector3 posE = transform.position;
        Vector3 posP = playerTS.position;
        Vector3 dir = new Vector3();
        Vector3 dirPos = new Vector3();

        // what direction does the raycast need to go
        dir.x = posP.x - posE.x;
        dir.y = posP.y - posE.y;

        // set positive for calculations
        if (dir.x > 0) { dirPos.x = dir.x * -1; } else dirPos.x = dir.x;
        if (dir.y > 0) { dirPos.y = dir.y * -1; } else dirPos.y = dir.y;

        // what is the distance between the player and the 
        float distance = Mathf.Sqrt((dirPos.x * dirPos.x) + (dirPos.y * dirPos.y));
        // is the layer close enough to check to throw or not
        if (distance < radius) { 
            // Raystuff hehe
            RaycastHit2D[] hit = Physics2D.RaycastAll(posE, dir, radius);
            Debug.DrawRay(posE, dir, Color.red,1f,false);
            if (hit.Length >=2)
            {
                if (hit[1].transform.gameObject.tag == "Player")
                {
                    enemyShoot.Shoot();
                }
            }
        }

        return false;
    }

    public void OnInteract()
    {

    }

    public void OnHit()
    {

    }
}
