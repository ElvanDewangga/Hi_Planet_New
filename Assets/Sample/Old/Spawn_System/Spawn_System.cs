using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_System : MonoBehaviour {
   [SerializeField]
   Spawn_System_Front_Camera _Spawn_System_Front_Camera;
    
    // Hi_Planet_Char_Data_Source :
   public void On_Spawn_System_Front_Camera (GameObject Object_Spawn, Camera Main_Camera) {
        /*
        Use this when you want to spawn object in front of camera
        Object_Spawn = Objek yang akan dispawn.
        Camera = target camera yang akan di spawn.
        */
        _Spawn_System_Front_Camera.On_Spawn_System_Front_Camera (Object_Spawn, Main_Camera);
   } 

    [SerializeField]
    Transform Titik_Pemain_Spawn;
    // Hi_Planet_Char_Data_Source :
    public void On_Titik_Pemain_Spawn (GameObject Object_Spawn) {
        Object_Spawn.transform.position = Titik_Pemain_Spawn.position;
    }
}
