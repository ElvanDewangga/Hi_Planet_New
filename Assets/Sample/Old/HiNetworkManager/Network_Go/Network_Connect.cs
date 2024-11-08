using System.Collections;
using System.Collections.Generic;
using FishNet.Example;
using UnityEngine;

public class Network_Connect : MonoBehaviour
{
    [SerializeField]
    NetworkHudCanvases _Network_Hud_Canvases;

    void Start () {
        _Network_Hud_Canvases.OnClick_Server ();
    }
}
