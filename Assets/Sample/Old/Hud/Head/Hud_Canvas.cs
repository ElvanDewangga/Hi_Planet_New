using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Canvas : MonoBehaviour {
    #region Player_Status_Mini
   [SerializeField]
   TMP_Text Nickname_Text;
        #region Refresh_Hp
        // Refresh tampilan Hp di Hud_Canvas.
        [SerializeField]
        TMP_Text Hp_Text;
            [SerializeField]
            int Max_Hp, Cur_Hp;
    
            public void On_Refresh_Cur_Hp (int x) {
                Cur_Hp = x;
                On_Refresh_Hp_Text ();
            }

            public void On_Refresh_Max_Hp (int x) {
                Max_Hp = x;
                On_Refresh_Hp_Text ();
            }

            void On_Refresh_Hp_Text () {
                Hp_Text.text = "Hp: " + Cur_Hp.ToString () +" / " +Max_Hp.ToString ();
            }
            #region Data_Game_Source
            public void On_Refresh_Username (string Username_V) {
                Nickname_Text.text = Username_V;
                
            }
            #endregion

            #region Char_Level
            public void On_Refresh_Level (int Level_V) {
                Level_Text.text = "Lvl. " + Level_V.ToString ();
            }

            public void On_Refresh_Exp (int Cur_Exp_V, int Max_Exp_V) {
                // Exp_Text.text = Cur_Exp_V.ToString () + " / " + Max_Exp_V.ToString ();
                if (Exp_Text_Follow_Value == null) {
                    Exp_Text_Follow_Value = Exp_Text.gameObject.GetComponent <Text_Follow_Value> ();
                } 
                Exp_Text_Follow_Value.On_Active (Cur_Exp_V,Max_Exp_V);

                Level_Slider.maxValue = Max_Exp_V;
                if (Level_Slider_Slider_Follow_Value == null) {
                    Level_Slider_Slider_Follow_Value = Level_Slider.gameObject.GetComponent <Slider_Follow_Value> ();
                }
                Level_Slider_Slider_Follow_Value.On_Active (Cur_Exp_V); 
            }
            #endregion
        #endregion
   [SerializeField]
   TMP_Text Level_Text;
   
   [SerializeField]
   Slider Level_Slider;
   Slider_Follow_Value Level_Slider_Slider_Follow_Value;
    [SerializeField]
    TMP_Text Exp_Text;
  Text_Follow_Value Exp_Text_Follow_Value;
 
   #endregion
   #region Claim_Button
     [SerializeField]
    Button Claim_Button;

    public void On_Claim_Button () {
        
        List <string> L_Name = new List<string> ();
        List <int> L_Quantity = new List<int> ();
        L_Name.Add (Name_Item); L_Quantity.Add (Quantity);
        All_Scene_Go.Ins._Inventory.On_Add_Item (L_Name, L_Quantity);
        Off_Got_Target ();
        Item_From.GetComponent <Item_Trigger> ().On_Destroy ();
    }
        #region Item_Trigger
        string Name_Item = "";
        int Quantity = 0;
        GameObject Item_From;
        public void On_Got_Target (GameObject Itg, string Name_Item_V, int Quantity_V) {
            Name_Item = Name_Item_V; Quantity = Quantity_V; Item_From = Itg;
            Claim_Button.gameObject.SetActive (true);
            
        }
        
        public void Off_Got_Target () {
            Name_Item =""; Quantity = 0;
            Claim_Button.gameObject.SetActive (false);
        }
        #endregion
   #endregion
}
