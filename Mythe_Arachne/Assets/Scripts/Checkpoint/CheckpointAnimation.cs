using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointAnimation : MonoBehaviour {

    private Animator animator;

    private bool interacted;

    //character animator
    private Animator anim;

    private int state;

    void Awake()
    {
        interacted = false; // reset
        animator = GetComponent<Animator>();

        state = animator.GetInteger("State");
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 12) return;
        if (interacted) return;

        interacted = true;

        NextAnimation();
    }

    private void NextAnimation()
    {
        state += 1;

        if (state > 3) state = 3;
        

        animator.SetInteger("State", state);
    }

}
