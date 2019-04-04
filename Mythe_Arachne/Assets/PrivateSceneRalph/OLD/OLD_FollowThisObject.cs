using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_FollowThisObject : MonoBehaviour {

    public Transform lockPos;
    public Vector3 ofset;
    
	// Update is called once per frame
	void Update () {

        transform.position = lockPos.transform.position + ofset;

	}
}
