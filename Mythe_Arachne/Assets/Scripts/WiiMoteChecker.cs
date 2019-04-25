using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_WIIU && !UNITY_ENGINE
using WiiU = UnityEngine.WiiU;
#endif

public class WiiMoteChecker : MonoBehaviour
{

#if UNITY_WIIU && !UNITY_ENGINE
    [SerializeField] [Range(0,3)] List<WiiU.Remote> remotes;
#endif
    private void Start()
    {
#if !UNITY_WIIU
        Debug.LogWarning("Wii IR Remotes can't be used on anything other than Wii U.\n" +
        "Removing component..");
        Destroy(GetComponent<WiiMoteInput>());
#endif

#if UNITY_WIIU && !UNITY_ENGINE
        for (int i = 0; i < 4; i++){
            remotes.Add(WiiU.Remote.Access(i));
        }
#endif

    }
}
