using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Visual_Novel_Kalimat {
    public string Name_Char;
   public string Dialog;
   public string Char_Event;
   public string System_Event_1;
   public string System_Event_2;
   public Visual_Novel_Kalimat () {

   }  
   public Visual_Novel_Kalimat (Visual_Novel_Kalimat d) {
        Name_Char = d.Name_Char;
        Dialog = d.Dialog;
        Char_Event = d.Char_Event;
        System_Event_1 = d.System_Event_1;
        System_Event_2 = d.System_Event_2;
   }
    /*
   #region Database_Online
   // Hall_Of_Masters :
   public int Got_Skill = 0; // 0 = Tidak ada / 1 = ada.
   #endregion
   */
}
