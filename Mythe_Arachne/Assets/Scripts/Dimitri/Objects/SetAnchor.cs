using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnchor : MonoBehaviour {

    void Update()
    {
        if (GetComponent<HingeJoint2D>() != null)
        {
            Rigidbody2D rig2d = GetComponent<HingeJoint2D>().connectedBody;
            if(rig2d != null)
                rig2d.GetComponent<RopePiece>().isAnchor = true;
        }
    }

}
