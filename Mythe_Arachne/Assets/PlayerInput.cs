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

//This section will only run on WiiU, It will make a reference to UnityEngine.WiiU.
#if UNITY_WIIU
using WiiU = UnityEngine.WiiU;
#endif

//This section will only run on N3DS, It will make a reference to UnityEngine.N3DS.
#if UNITY_N3DS
using N3DS = UnityEngine.N3DS;
#endif


public class PlayerInput : MonoBehaviour
{
    public Vector2 JoystickMove { get; private set; }
    public bool JumpInput { get; private set; }

#if UNITY_WIIU
    WiiU.GamePad gamePad;
#endif

    private void Start()
    {
#if UNITY_WIIU && !UNITY_EDITOR
        gamePad = WiiU.GamePad.access;
#endif       
    }

    private void Update()
    {
#if UNITY_WIIU && !UNITY_EDITOR
        WiiU.GamePadState gamePadState = gamePad.state;

        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            JumpInput = gamePadState.IsTriggered(WiiU.GamePadButton.A);
            JoystickMove = gamePadState.lStick;
        }
        else
        {
            Debug.Log("The gamepad is required to play.");
        }
#endif


#if UNITY_N3DS && !UNITY_EDITOR
        JumpInput = N3DS.GamePad.GetButtonTrigger(N3dsButton.A);
        JoystickMove = N3DS.GamePad.CirclePad;
#endif


#if UNITY_EDITOR
        JoystickMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        JumpInput = Input.GetKeyDown("space");
#endif
    }
}
