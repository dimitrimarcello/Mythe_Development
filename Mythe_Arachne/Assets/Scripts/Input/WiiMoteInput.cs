﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_WIIU
using UnityEngine.WiiU;
#endif

public class WiiMoteInput : MonoBehaviour
{

#if UNITY_WIIU && !UNITY_ENGINE
    [SerializeField] [Range(0,4)] int ID;

    UnityEngine.WiiU.Remote thisRemote;
#endif

    void Awake()
    {
#if !UNITY_WIIU
        Debug.LogWarning("Wii IR Remotes can't be used on anything other than Wii U.\n" +
        "Removing component..");
        Destroy(GetComponent<WiiMoteInput>());
#endif
    }


#if UNITY_WIIU && !UNITY_ENGINE
    void Start()
    {
        ID = 0;
        thisRemote = UnityEngine.WiiU.Remote.Access(ID);

    }

    void Update()
    {
        UnityEngine.WiiU.RemoteState remoteState = thisRemote.state;

    }
#endif
}
