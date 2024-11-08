using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Control : MonoBehaviour {
    public static Control instance;
    void Awake () {
        instance = this;
    }
    public DualJoystickPlayerController dualJoystickPlayerController;
   
    // DualJoystickPlayerController
    public void OnAim () {
        // Char_Data.Ins._Char_Data_Technique._Aiming_Direction.On_Aiming ();
        AccountManager.instance.player._Aiming_Direction.On_Aiming ();
    }
    
    public void OffAim () {
        // Char_Data.Ins._Char_Data_Technique._Aiming_Direction.Off_Aiming ();
         AccountManager.instance.player._Aiming_Direction.Off_Aiming ();
    }

    public void OnAimingDirectionObjectRotateAround (Vector3 Direction_V) {
       // Char_Data.Ins._Char_Data_Technique._Aiming_Direction.distanceVisualizer.On_Aiming_Direction_Object_Rotate_Around (Direction_V);
         AccountManager.instance.player._Aiming_Direction.distanceVisualizer.On_Aiming_Direction_Object_Rotate_Around (Direction_V);
    }
}
