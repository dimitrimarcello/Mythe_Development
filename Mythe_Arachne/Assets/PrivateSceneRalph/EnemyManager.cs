using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public float timer_end = 10, timer_cur = 0f;
    public float entangledTime = 3f;
    private bool entangled = false;
    private bool diveTime = false;

    private EnemyMovement en_mov;
    private EnemyAttack en_att;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        en_mov = GetComponent<EnemyMovement>();
        en_att = GetComponent<EnemyAttack>();
        ColliderTrigger.Hit += EnemyGotHit;

    }

    // Update is called once per frame
    void Update () {
        if (!entangled)
        {
            en_att.EnemyUpdate();
            if (en_mov.UnderMe(en_att.range) && en_att.IsDiving() == false)
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

    private void EnemyGotHit()
    {
        if (!entangled)
        Tangle();

    }

    IEnumerator Tangle()
    {
        // set tangle stats
        anim.SetBool("Tangled", true);
        entangled = true;

        yield return new WaitForSeconds(entangledTime);
        // return to normal
        anim.SetBool("Tangled", false);
        entangled = false;

    }
}
