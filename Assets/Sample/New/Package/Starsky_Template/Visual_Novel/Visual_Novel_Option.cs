using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starsky;
using System;
public class Visual_Novel_Option : MonoBehaviour {
  [SerializeField]
  GameObject Vn_Option_Viewport;

  [SerializeField]
  Transform Vn_Option_Layout;

  [SerializeField]
  GameObject Vn_Option_Sample;

  // Visual_Novel_Event :  
  public void On_Vn_Option (List <string> L_Title, List <string> L_Event) {
     /*
       Star_GameObject St = new Star_GameObject();
       Vn_Option_Layout.GetComponent <Panel_Destroyer> ().On_Destroy_All_Clone ();
       int i = 0;
       for (i = 0; i < L_Title.Count; i ++) {
            St.On_Instantiate (Vn_Option_Sample,Vn_Option_Layout, true);
            St.On_Input_Method ("Clone_Sample","On_Set_b_Clone",new Type[] { typeof(bool)}, new object[] {true});
            St.On_Input_Method ("Button_Event","On_Set_Title",new Type[] { typeof(string)}, new object[] {L_Title[i]});       
            St.On_Input_Method ("Button_Event","On_Set_Event",new Type[] { typeof(string)}, new object[] {"Visual_Novel_Option"});
            St.On_Input_Method ("Button_Sub_Event","On_Set_Sub_Event",new Type[] { typeof(string)}, new object[] {L_Event[i]});
       } 
       Vn_Option_Viewport.gameObject.SetActive (true);
       Vn_Option_Viewport.GetComponent <Scroll_Rect_Position> ().ScrollToTopPosition ();
     */
  }

  // Visual_novel_System :   
  public void Off_Vn_Option () {
     Vn_Option_Viewport.gameObject.SetActive (false);
  }

  // Button_Event_Server :   
  public void On_Answer_Option (string Code_Answer) {
      List <Visual_Novel_Kalimat> L_Kalimat = Visual_Novel_System.Ins.On_Get_L_Visual_Novel_Kalimat (); 
      int x = 0;
      for (x =0; x < L_Kalimat.Count; x++) {
          if (L_Kalimat[x].System_Event_1.Contains(Code_Answer) && L_Kalimat[x].Name_Char.Contains("(Action)")) {
               Visual_Novel_System.Ins.On_Answer_Option ((x+1)); // +1 langsung lanjut dialog dibawah (Action)
               return;
          }
      }
  }
}
