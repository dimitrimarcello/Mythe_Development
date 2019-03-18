using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadCamera : MonoBehaviour {

    [SerializeField]GameObject Player;
    PlayerInput pInput;

    private void FixedUpdate()
    {
        transform.position = Player.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, pInput.GyroInput.z);
    }
}
