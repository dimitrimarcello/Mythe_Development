using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TempMovement : MonoBehaviour {

	public float Speed;
	public float jumpHeight;
	private Rigidbody2D playerMove;

	void Start()
	{
		playerMove = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.A)){
			// playerMove.AddForce(-transform.right * Speed, ForceMode2D.Force);
			playerMove.velocity =  new Vector2(0,playerMove.velocity.y);
			transform.Translate(-transform.right * Time.deltaTime * Speed);
		}
        if (Input.GetKey(KeyCode.D))
        {
            // playerMove.AddForce(transform.right * Speed, ForceMode2D.Force);
            playerMove.velocity = new Vector2(0, playerMove.velocity.y);
            transform.Translate(transform.right * Time.deltaTime * Speed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMove.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            // transform.Translate(-transform.right * Speed);
        }
	}

}
