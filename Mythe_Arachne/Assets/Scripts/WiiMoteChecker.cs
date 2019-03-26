using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class WiiMoteChecker : MonoBehaviour
{
    [SerializeField] [Range(0,3)] List<WiiU.Remote> remotes;

    private void Start()
    {
#if !UNITY_WIIU
        Debug.LogWarning("Wii IR Remotes can't be used on anything other than Wii U.\n" +
        "Removing component..");
        Destroy(GetComponent<WiiMoteInput>());
#endif
        for (int i = 0; i < 4; i++){
            remotes.Add(WiiU.Remote.Access(i));
        }

    }

    void Update()
    {

    }
}
