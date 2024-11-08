using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_System_Front_Camera : MonoBehaviour {
    /*
    void Example_On_Spawn_System_Front_Camera () {

    }
    */

    public void On_Spawn_System_Front_Camera (GameObject Object, Camera Main_Camera) {
        Object.transform.position = Main_Camera.transform.position;
    }
}
