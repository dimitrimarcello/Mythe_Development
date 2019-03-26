////////////////////////////////////
//THIS SCRIPT IS FOR TESTING ONLY///
//////////////////////////////////// -Sjors

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TestGyroInput : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] Text Values;

    private void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        Values = GetComponent<Text>();
    }

    void Update()
    {
        Values.text = playerInput.GyroInput.ToString();
    }
}
