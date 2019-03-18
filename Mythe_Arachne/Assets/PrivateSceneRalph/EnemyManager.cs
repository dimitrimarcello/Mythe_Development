using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public float timer_end = 10, timer_cur;
    private bool diveTime = false;

    private EnemyMovement en_mov;
    private EnemyAttack en_att; 

	// Use this for initialization
	void Start () {
        en_mov = GetComponent<EnemyMovement>();
        en_att = GetComponent<EnemyAttack>();
	}
	
	// Update is called once per frame
	void Update () {
        if (en_mov.UnderMe())
        {
            timer_cur += Time.deltaTime;
        }
        if (en_att.IsDiving() == false)
        {
            diveTime = false;
            en_mov.UpdateMovement();
        } 

        if (timer_cur >= timer_end && diveTime == false)
        {
            en_att.Dive();
            diveTime = true;
            timer_cur = 0;
        }

        
	}
}
