using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public Vector3 checkPointPos { get { return transform.position; } }
    private Animator playAnimations;
    [SerializeField]
    private string animName;

    private void Start()
    {
        playAnimations = GetComponent<Animator>();
        CheckPointManager subscribe = GameObject.FindObjectOfType<CheckPointManager>();
        subscribe.CheckPointChanged += PlayAnimation;
    }

    private void PlayAnimation()
    {
        if(playAnimations != null)
        {
            playAnimations.Play(animName);
        }
    }

}
