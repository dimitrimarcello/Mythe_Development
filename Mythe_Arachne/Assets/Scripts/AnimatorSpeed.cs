using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSpeed : MonoBehaviour {

    [SerializeField] PlayerInput input;
    [SerializeField] Animator CharacterSpeed;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        CharacterSpeed = GetComponent<Animator>();
    }

    void FixedUpdate () {
        if (input.JoystickMove.x > 0 || input.JoystickMove.x < 0)
        {
            CharacterSpeed.speed = Mathf.Abs(input.JoystickMove.x);
        }
        else
        {
            CharacterSpeed.speed = 1;
        }
	}
}
