using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating_delete : MonoBehaviour {

    [SerializeField]
    private float maxY;

    [SerializeField]
    float minY;

    private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

        float y = rigidbody.transform.position.y;

        if (y > maxY)
        {
            rigidbody.gravityScale = 1;
        } else

        if (y < minY)
        {
            rigidbody.gravityScale = -1;
        }
		
	}
}
