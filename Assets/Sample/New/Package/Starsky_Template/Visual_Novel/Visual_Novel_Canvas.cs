using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
public class Visual_Novel_Canvas : MonoBehaviour {
   #region Head
   [SerializeField]
   Canvas _Visual_Novel_Canvas;
   [SerializeField]
   Animator_Script _Animator_Script;
   // Visual_Novel_System :
   public void On_Start_Visual_Novel_Read () {
     // if (_Animator_Script == null) {_Animator_Script = _Visual_Novel_Canvas.gameObject.GetComponent<Animator_Script>(); }
      _Visual_Novel_Canvas.gameObject.SetActive (true);
      On_Show_All_Button ();
      
   }
      #region Close_Visual_Novel
      // Visual_Novel_System :
      public void On_Close_Visual_Novel () {
         _Animator_Script.On_Set_Animation ("Fade_Out");
         _Animator_Script.On_Set_Active (1.0f, _Visual_Novel_Canvas.gameObject, false);
        //  _Visual_Novel_Canvas.gameObject.SetActive (false);
         Off_Show_All_Button ();
      }
      #endregion

   #endregion
   #region Name
   [SerializeField]
   GameObject Bg_Name;
   [SerializeField]
   TMP_Text Name_Tx;

   // Visual_Novel_System :
   public void On_Set_Name_Tx (string Name_V) {
      if (Name_V == "x" || Name_V =="(Narator)") {
         Name_Tx.text = "";
      } else {
        // Name_Tx.text = All_Scene_Go_Script.Ins._Pengubah_String._TMP_Text_Pengubah_Text_Sumber.On_TMP_Text_Pengubah_Text (Name_V);
      }
   }
   #endregion
   #region Dialog
   [SerializeField]
   TMP_Text Dialog_Tx;

   // Visual_Novel_System :
   public void On_Set_Dialog_Tx (string Dialog_V) {
      // Dialog_Tx.text = All_Scene_Go_Script.Ins._Pengubah_String._Replace_String.On_Replace_String (Dialog_V, "Player_Child", All_Scene_Go_Script.Ins._Pengubah_String._TMP_Text_Pengubah_Text_Sumber.On_TMP_Text_Pengubah_Text ("Player_Child") );
      Visual_Novel_System.Ins._Visual_Novel_Dialog_Fade.On_Start ();
   }

   #endregion
   #region Button
   [SerializeField]
   Button Invisible_Next_Button;
   // Visual_Novel_Event, this :
   public void Off_Show_All_Button () {
      Invisible_Next_Button.gameObject.SetActive (false);
   }
   // Visual_Novel_System, this :
   public void On_Show_All_Button () {
      Invisible_Next_Button.gameObject.SetActive (true);
   }
   #endregion

}
