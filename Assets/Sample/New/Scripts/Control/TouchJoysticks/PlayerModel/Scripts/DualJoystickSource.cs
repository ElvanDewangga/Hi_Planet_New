using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualJoystickSource : MonoBehaviour {
   // DualJoystickPlayerController
    public void On_Aim () {
        // Char_Data.Ins._Char_Data_Technique._Aiming_Direction.On_Aiming ();
        Char_Data.Ins.Your_Char_Utama_Script._Aiming_Direction.On_Aiming ();
    }
    
    public void Off_Aim () {
        // Char_Data.Ins._Char_Data_Technique._Aiming_Direction.Off_Aiming ();
        Char_Data.Ins.Your_Char_Utama_Script._Aiming_Direction.Off_Aiming ();
    }

    public void On_Aiming_Direction_Object_Rotate_Around (Vector3 Direction_V) {
       // Char_Data.Ins._Char_Data_Technique._Aiming_Direction.distanceVisualizer.On_Aiming_Direction_Object_Rotate_Around (Direction_V);
        Char_Data.Ins.Your_Char_Utama_Script._Aiming_Direction.distanceVisualizer.On_Aiming_Direction_Object_Rotate_Around (Direction_V);
    }
    
}
