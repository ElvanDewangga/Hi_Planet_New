using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_Tab_Samp : MonoBehaviour {
     public List <GameObject> L_Go_Tab;
    // Size_Control_Input_Data :
   public void On_Add_Go_Tab (GameObject s) {
    L_Go_Tab.Add (s);
   }
   public void On_Remove_Go_Tab (GameObject s) {
    L_Go_Tab.Remove (s);
   }    
}
