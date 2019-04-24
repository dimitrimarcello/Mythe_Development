using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadCamera : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float power = .05f;
    [SerializeField] bool FocusPlayer;

    private void FixedUpdate()
    {
        SmoothMover();
    }

    void SmoothMover()
    {
        float magnifier = Mathf.Abs(transform.position.x - Player.transform.position.x);
        transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y, -10), power * magnifier);

        if (FocusPlayer)
            transform.LookAt(Player.transform);
    }
}
