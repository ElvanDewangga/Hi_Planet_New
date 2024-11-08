using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_Button : MonoBehaviour {
   // Attach to Button :
   public void On_Cli () {
      // All_Scene_Go_Script.Ins._Back_Menu.On_Close_Menu ();  
   }

   void On_Example () {
     // All_Scene_Go_Script.Ins._Popup_Type._Close_Button.On_Active_Close_Button (Panel_Layout_Close_Button);
     On_Active_Close_Button (this.transform);
     Off_Active_Close_Button ();
   }
   #region Diactive_Close_Button
   public void Off_Active_Close_Button () {
        // this.transform.SetParent (All_Scene_Go_Script.Ins._Popup_Type.Popup_Panel);
        this.gameObject.SetActive (false);
   } 
   #endregion
   #region Active_Close_Button
   public void On_Active_Close_Button (Transform Parent_Layout) {
        this.transform.SetParent (Parent_Layout);
        this.gameObject.SetActive (true);
   } 
   #endregion
}
