using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadCamera : MonoBehaviour
{
    [SerializeField] GameObject Player;
    PlayerInput pInput;
    float Speed = 0;

    private void Start()
    {
        pInput = Player.GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        SmoothMover();
        transform.rotation = Quaternion.Euler(0, 0, pInput.GyroInput.z);
    }

    void SmoothMover()
    {
        if (Mathf.Round(transform.position.x) != Mathf.Round(Player.transform.position.x) || Mathf.Round(transform.position.y) != Mathf.Round(Player.transform.position.y))
        {
            Speed += 0.01f / 100;
            transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y, -10), Speed);
        }
        else
        {
            if (Speed > 0.0001)
            {
                Speed -= 0.01f * 10;
            }
        }
        if (Speed > 0.1f)
        {
            Speed = 0.1f;
        }
        if (Speed < 0.0001)
        {
            Speed = 0.0001f;
        }
    }
}
