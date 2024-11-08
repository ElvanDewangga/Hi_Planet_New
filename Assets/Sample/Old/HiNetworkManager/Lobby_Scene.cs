using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby_Scene : MonoBehaviour {
    public static Lobby_Scene Ins;
    public Network_Go _Network_Go;

    void Awake () {
        Ins = this;
    }
}
