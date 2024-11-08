using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Button : MonoBehaviour {
   public void Test_1 () {
    Loading.Ins.On_Loading_Time ("Black_Fade", 1.5f);
    Loading.Ins.On_Loading_Animation_Event_A_Object (All_Scene_Go.Ins._Notification_Script._World_Place_Name.On_World_Place_Home, new object [] {"Ecopolis"}, 1.5f);
   }

   public void Test_Exp () {
      Char_Data.Ins.Your_Char_Utama.GetComponent <Char_Utama> ()._Char_Level.On_Inc_Exp (15);
   }
}
