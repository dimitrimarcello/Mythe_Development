//////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////
/////// This script has been build to be the input for the game, all but touch! :) ///////
//////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////// -Sjors

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This section will only run on WiiU.
#if UNITY_WIIU && !UNITY_ENGINE
using UnityEngine.WiiU;
#endif

//This section will only run on N3DS.
#if UNITY_N3DS
using UnityEngine.N3DS;
#endif

public class PlayerInput : MonoBehaviour
{
    //Values that can be get but not set by other scripts
    public bool MouseInput { get; private set; }
    public Vector2 MousePosition { get; private set; }
    public Vector2 JoystickMove { get; private set; }
    public Vector3 GyroInput { get; private set; }
    public bool AY { get; private set; }
    public bool BX { get; private set; }
    public bool ZLZR { get; private set; }

#if UNITY_WIIU
    UnityEngine.WiiU.GamePad gamePad;
#endif

    private void Start()
    {
#if UNITY_WIIU && !UNITY_EDITOR
        //Set the gamepad
        gamePad = UnityEngine.WiiU.GamePad.access;
#endif       
    }

    private void Update()
    {
        //Depending on the platform it will pick what will run
#if UNITY_WIIU && !UNITY_EDITOR
        WiiU();
#endif

#if UNITY_N3DS && !UNITY_EDITOR
        N3DS();
#endif

#if UNITY_EDITOR 
        EDITOR();
#endif
        GetMouse();
    }

#if UNITY_WIIU && !UNITY_EDITOR
    void WiiU()
    {
        UnityEngine.WiiU.GamePadState gamePadState = gamePad.state;
        
        //Look if the gamepad is on, ifso check for inputs, if not, do nothing
        if (gamePadState.gamePadErr == UnityEngine.WiiU.GamePadError.None)
        {
            AY = gamePadState.IsTriggered(UnityEngine.WiiU.GamePadButton.A) || gamePadState.IsTriggered(UnityEngine.WiiU.GamePadButton.Y);
            BX = gamePadState.IsTriggered(UnityEngine.WiiU.GamePadButton.B) || gamePadState.IsTriggered(UnityEngine.WiiU.GamePadButton.X);
            ZLZR = gamePadState.IsTriggered(UnityEngine.WiiU.GamePadButton.ZL) || gamePadState.IsTriggered(UnityEngine.WiiU.GamePadButton.ZR);
            JoystickMove = gamePadState.lStick;
            GyroInput = gamePadState.gyro;
        }
        else
        {
            Debug.Log("The gamepad is required to play.");
        }
    }
#endif

#if UNITY_N3DS && !UNITY_EDITOR
    void N3DS()
    {
        //Look for input
        AY = UnityEngine.N3DS.GamePad.GetButtonTrigger(N3dsButton.A) || UnityEngine.N3DS.GamePad.GetButtonTrigger(N3dsButton.Y);
        BX = UnityEngine.N3DS.GamePad.GetButtonTrigger(N3dsButton.B) || UnityEngine.N3DS.GamePad.GetButtonTrigger(N3dsButton.X);
        ZLZR = UnityEngine.N3DS.GamePad.GetButtonTrigger(N3dsButton.ZL) || UnityEngine.N3DS.GamePad.GetButtonTrigger(N3dsButton.ZR);
        JoystickMove = UnityEngine.N3DS.GamePad.CirclePad;
        GyroInput = Input.gyro.rotationRate;
    }
#endif

#if UNITY_ANDROID
    void ANDROID()
    {
        //maybe someday
        GyroInput = Input.gyro.rotationRate;
    }
#endif

#if UNITY_EDITOR
    void EDITOR()
    {
        //Look for the input
        JoystickMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        ZLZR = Input.GetKeyDown("z");
        AY = Input.GetKeyDown("x");
        BX = Input.GetKeyDown("c");
    }
#endif

    void GetMouse()
    {
        MouseInput = Input.GetMouseButtonDown(0);
        //On click, get mouse position
        if (MouseInput)
        {
            MousePosition = Input.mousePosition;
        }
    }
}
