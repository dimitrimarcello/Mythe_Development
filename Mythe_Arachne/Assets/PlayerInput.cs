//////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////
/////// This script has been build to be the input for the game, all but touch! :) ///////
//////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////// -Sjors

/*        
____________________________
|__________________________|
||    * - Changes - *     ||
||========================||
|| ~25-2-2019 By Sjors ~  ||
|| + Basic input for WiiU,||
|| 3DS and EDITOR.        ||
|| - POS on JOYSTICK INPUT||
||  (for testing)         ||
||________________________||

Please note any changes, Thank you! ^-^*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This section will only run on WiiU.
#if UNITY_WIIU
using UnityEngine.WiiU;
#endif

//This section will only run on N3DS.
#if UNITY_N3DS
using UnityEngine.N3DS;
#endif

public class PlayerInput : MonoBehaviour
{
    public Vector2 JoystickMove { get; private set; }
    public Vector3 GyroInput { get; private set; }
    public bool AB { get; private set; }
    public bool ZLZR { get; private set; }

#if UNITY_WIIU
    UnityEngine.WiiU.GamePad gamePad;
#endif

    private void Start()
    {
#if UNITY_WIIU && !UNITY_EDITOR
        gamePad = UnityEngine.WiiU.GamePad.access;
#endif       
    }

    private void Update()
    {
#if UNITY_WIIU && !UNITY_EDITOR
        WiiU();
#endif

#if UNITY_N3DS && !UNITY_EDITOR
        N3DS();
#endif

#if UNITY_EDITOR 
        EDITOR();
#endif
    }

#if UNITY_WIIU && !UNITY_EDITOR
    void WiiU()
    {
        UnityEngine.WiiU.GamePadState gamePadState = gamePad.state;

        if (gamePadState.gamePadErr == UnityEngine.WiiU.GamePadError.None)
        {
            AB = gamePadState.IsTriggered(UnityEngine.WiiU.GamePadButton.A) || gamePadState.IsTriggered(UnityEngine.WiiU.GamePadButton.B);
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
        AB = UnityEngine.N3DS.GamePad.GetButtonTrigger(N3dsButton.A) || UnityEngine.N3DS.GamePad.GetButtonTrigger(N3dsButton.B);
        ZLZR = UnityEngine.N3DS.GamePad.GetButtonTrigger(N3dsButton.ZL) || UnityEngine.N3DS.GamePad.GetButtonTrigger(N3dsButton.ZR);
        JoystickMove = UnityEngine.N3DS.GamePad.CirclePad;
    }
#endif

#if UNITY_EDITOR
    void EDITOR()
    {
        JoystickMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        ZLZR = Input.GetKeyDown("z");
        AB = Input.GetKeyDown("x");
    }
#endif
}
