using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedToAnimation : MonoBehaviour {

	public Animator setValues;
	private Rigidbody2D getValues;
    private PlayerInput getButtons;
    private PlayerMovement getStatus;
    private bool moving = false;

    void Awake()
    {
        getValues = GetComponent<Rigidbody2D>();
        getButtons = GetComponent<PlayerInput>();
        getStatus = GetComponent<PlayerMovement>();
    }

	void Update()
	{
        if(getButtons.JoystickMove.x > 0 || getButtons.JoystickMove.x < 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
		setValues.SetBool("Grounded", getStatus.Grounded);
        setValues.SetBool("Moving", moving);
        setValues.SetFloat("YAxis", getValues.velocity.y);
	}

}
