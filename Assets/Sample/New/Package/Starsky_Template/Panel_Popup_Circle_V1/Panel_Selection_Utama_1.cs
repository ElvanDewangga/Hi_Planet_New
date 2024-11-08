using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Selection_Utama_1 : MonoBehaviour {
   string Event = "";
   [SerializeField]
   Partna_Select_Button [] A_Partna_Select_Button;
   List <Partna_Select_Button> L_Hubungan_Button = new List<Partna_Select_Button> ();
   // [0] = Avatar_Button [1] = Medal_Button [2] = Save_Button
   // [3] = Left_Partna, [4] = Mid_Partna, [5] = Right_Partna
   public void On_Panel (string Event_V) { // Header_Script
        Event = Event_V;
        L_Hubungan_Button = new List<Partna_Select_Button> ();
        if (Event == "Player_Profile_Change") {
           L_Hubungan_Button.Add (A_Partna_Select_Button[0]); A_Partna_Select_Button[0].gameObject.SetActive (true); A_Partna_Select_Button[0].On_Input_Data ("Player_Profile_Change_Header", "Avatar_Button");
           L_Hubungan_Button.Add (A_Partna_Select_Button[1]); A_Partna_Select_Button[1].gameObject.SetActive (true); A_Partna_Select_Button[1].On_Input_Data ("Player_Profile_Change_Header", "Medal_Button");
           L_Hubungan_Button.Add (A_Partna_Select_Button[3]); A_Partna_Select_Button[3].gameObject.SetActive (true); A_Partna_Select_Button[3].On_Input_Data ("Player_Profile_Change_Header", "Left_Partna");
           L_Hubungan_Button.Add (A_Partna_Select_Button[4]); A_Partna_Select_Button[4].gameObject.SetActive (true); A_Partna_Select_Button[4].On_Input_Data ("Player_Profile_Change_Header", "Mid_Partna");
           L_Hubungan_Button.Add (A_Partna_Select_Button[5]); A_Partna_Select_Button[5].gameObject.SetActive (true); A_Partna_Select_Button[5].On_Input_Data ("Player_Profile_Change_Header", "Right_Partna");
 
            A_Partna_Select_Button[2].gameObject.SetActive (true); A_Partna_Select_Button[2].On_Input_Data ("Player_Profile_Change_Footer", "Save_Button");

            L_Hubungan_Button[0].Cli_Button ();
        }
        this.gameObject.SetActive (true);
   }

   public void Off_Panel () { // Header_Script
        foreach (Partna_Select_Button But in A_Partna_Select_Button) {
            if (But.gameObject.activeInHierarchy) {
                But.gameObject.SetActive (false);
            }
        }
        this.gameObject.SetActive (false);
   }

   #region L_Hubungan_Button
    // Partna_Select_Button :
    public void On_Refresh_Hubungan_Button (Partna_Select_Button Tar) {
        foreach (Partna_Select_Button Ps in L_Hubungan_Button) {
            if (Ps != Tar) {
                Ps.Off_Select_Directly ();
            } else {
                Ps.On_Select_Directly_2 ();
            }
        }
    }
   #endregion
   
}
