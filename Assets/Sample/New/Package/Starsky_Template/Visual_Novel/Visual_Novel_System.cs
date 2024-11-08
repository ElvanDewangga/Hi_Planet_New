using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual_Novel_System : MonoBehaviour {
   
   public static Visual_Novel_System Ins;

   void Awake () {
    Ins = this;
   }
   public Visual_Novel_World _Visual_Novel_World;
   public Visual_Novel_Canvas _Visual_Novel_Canvas;
   public Visual_Novel_Option _Visual_Novel_Option;
   public Visual_Novel_Sprite _Visual_Novel_Sprite;
   public Visual_Novel_Dialog_Fade _Visual_Novel_Dialog_Fade;
   #region Start_Visual_Novel
      #region Example
      public void On_Example_Start_Visual_Novel () { // Button Test
      
         On_Start_Visual_Novel ("Visual_Novel_Res/Opening_Act", "Visual_Novel_Kalimat");
       //  On_Start_Visual_Novel ("Visual_Novel_Res/Opening_Act", "Visual_Novel_Kalimat");
    //     On_Start_Visual_Novel ("Visual_Novel_Res/First_Home", "Visual_Novel_Kalimat");
        // On_Start_Visual_Novel_Read ();
      }

      #endregion
   /* Available :
   On_Start_Visual_Novel ("Visual_Novel_Res/Dialog_Test", "Visual_Novel_Kalimat"); : this
   On_Start_Visual_Novel ("Visual_Novel_Res/Opening_Act", "Visual_Novel_Kalimat"); : Character_Status_Sumber
   On_Start_Visual_Novel ("Visual_Novel_Res/First_Home", "Visual_Novel_Kalimat") : 
   
   */
   

   public void On_Start_Visual_Novel (string Location, string Type_V) {
      On_Refresh_L_Visual_Novel_Kalimat (); 
      Load_Csv.Ins.On_Load_Data (Location, Type_V);
      On_Start_Visual_Novel_Read ();
      if (b_On_Start_Visual_Novel_From_Event) {
      StartCoroutine (Off_Loading ());
      }
   }

   IEnumerator Off_Loading () {
      yield return new WaitForSeconds (1.5f);
      Loading.Ins.Off_Loading ("Loading_1");
      b_On_Start_Visual_Novel_From_Event = false;
   }
   #endregion
   // Regex_Format_1_Event :
   public Visual_Novel_Event _Visual_Novel_Event;

   #region Visual_Novel_Kalimat
   [SerializeField]
   List <Visual_Novel_Kalimat> L_Visual_Novel_Kalimat;
   public List <Visual_Novel_Kalimat> On_Get_L_Visual_Novel_Kalimat () {
      return L_Visual_Novel_Kalimat;
   }
   public void On_Add_L_Visual_Novel_Kalimat (Visual_Novel_Kalimat s) {
      L_Visual_Novel_Kalimat.Add (s);
   }

   void On_Refresh_L_Visual_Novel_Kalimat () {
      L_Visual_Novel_Kalimat.Clear ();
   }
   #endregion
   #region Visual_Novel_Read
   int Cur_Read = 0;
   Regex_Utama.Regex_Format_1 Rgx_Event_1 = new Regex_Utama.Regex_Format_1 ();
   void On_Start_Visual_Novel_Read () {
      
      Cur_Read = 0;
      _Visual_Novel_World.On_Start_Visual_Novel_Read ();
      _Visual_Novel_Canvas.On_Start_Visual_Novel_Read ();
      On_Doing_Visual_Novel_Read ();
   }

   void On_Doing_Visual_Novel_Read () {
      Rgx_Event_1 = new Regex_Utama.Regex_Format_1 ();
      Rgx_Event_1.On_Get_List_From_Format_1 (L_Visual_Novel_Kalimat[Cur_Read].Char_Event);
      Rgx_Event_1.On_Doing_Event ();

      _Visual_Novel_Event.On_Check_Start_Event (L_Visual_Novel_Kalimat[Cur_Read].System_Event_1);
      Rgx_Event_1 = new Regex_Utama.Regex_Format_1 ();
      Rgx_Event_1.On_Get_List_From_Format_1 (L_Visual_Novel_Kalimat[Cur_Read].System_Event_1);
      Rgx_Event_1.On_Doing_Event ();

      if (On_Check_Dialog_X (L_Visual_Novel_Kalimat[Cur_Read].Name_Char)) {
         On_Next_Visual_Novel_Read ();
         return;
      } 


      

      _Visual_Novel_Canvas.On_Set_Name_Tx (L_Visual_Novel_Kalimat[Cur_Read].Name_Char);
      _Visual_Novel_Canvas.On_Set_Dialog_Tx (L_Visual_Novel_Kalimat[Cur_Read].Dialog);

      
   }

   // But : Invisible_Next_Button
   bool b_Respawn_Next_Button = false;
   public void On_Next_Visual_Novel_Read () {
      if (!b_Respawn_Next_Button ) {
         b_Respawn_Next_Button = true;
         if ( _Visual_Novel_Dialog_Fade.IsTextFadeComplete ()) {
          if (!On_Check_Answer_Option (L_Visual_Novel_Kalimat[Cur_Read].System_Event_1)) {
      
               Cur_Read ++;
               On_Doing_Visual_Novel_Read ();
              
          }
         } else {
           Visual_Novel_System.Ins._Visual_Novel_Dialog_Fade.On_Direct_Show_All_Text ();
         }  
         StartCoroutine (N_On_Next_Visual_Novel_Read ());
      }
   }

   IEnumerator N_On_Next_Visual_Novel_Read () {
      yield return new WaitForSeconds (0.1f);
      b_Respawn_Next_Button = false;
   }
      #region Dialog_X
      bool On_Check_Dialog_X (string Name_Char) {
         bool Res = false;
         if (Name_Char == "x" && !L_Visual_Novel_Kalimat[Cur_Read].System_Event_1.Contains ("Close_Visual_Novel")) {
            Res = true;
         }
         return Res;
      }
      #endregion

      #region On_Check_Answer_Option
      public bool On_Check_Answer_Option (string System_Event_1) {
         bool Res = false;
         if (System_Event_1.Contains ("Answer_Option")) {
            Res = true;
         }
         return Res;
      }
      #endregion
   #endregion
   #region Visual_Novel_Option
   public void On_Answer_Option (int Lanjut_Code) {
      _Visual_Novel_Option.Off_Vn_Option ();
      Cur_Read = Lanjut_Code;
      _Visual_Novel_Canvas.On_Show_All_Button ();
      On_Doing_Visual_Novel_Read ();
   }
   #endregion
   #region Close_Visual_Novel

   public void On_Close_Visual_Novel () {
     // Debug.Log ("Close Vn");
      _Visual_Novel_World.On_Close_Visual_Novel ();
      _Visual_Novel_Canvas.On_Close_Visual_Novel (); 
   }
   #endregion
   #region On_Start_Visual_Novel_From_Event
   bool b_On_Start_Visual_Novel_From_Event = false;
   // Visual_Novel_Event :
   public void On_Start_Visual_Novel_From_Event (string Line_2) {
      
      Loading.Ins.On_Loading ("Loading_1");
      b_On_Start_Visual_Novel_From_Event = true;
      On_Start_Visual_Novel ("Visual_Novel_Res/" + Line_2, "Visual_Novel_Kalimat");
   }
   #endregion
}
