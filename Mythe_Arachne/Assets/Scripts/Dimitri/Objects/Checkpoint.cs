using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public Vector3 checkPointPos { get { return transform.position; } }
    private Animator playAnimations;
    [SerializeField]
    private Sprite broken;
    private Sprite unBroken;
    private ParticleSystem brokenEffect;
    private bool hasBroken = false;
    CheckPointManager getInfo;

    private void Start()
    {
        playAnimations = GetComponent<Animator>();
        getInfo = GameObject.FindObjectOfType<CheckPointManager>();
        unBroken = GetComponent<SpriteRenderer>().sprite;
        brokenEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if(getInfo.lastCheckpoint == GetComponent<Checkpoint>())
        {
            PlayAnimation();
        }
        else
        {
            FixState();
        }
    }

    private void PlayAnimation()
    {
        if(brokenEffect != null && !hasBroken)
        {
            brokenEffect.Play();
            GetComponent<SpriteRenderer>().sprite = broken;
            hasBroken = true;
        }
    }

    private void FixState()
    {
        GetComponent<SpriteRenderer>().sprite = unBroken;
        hasBroken = false;
    }

}
