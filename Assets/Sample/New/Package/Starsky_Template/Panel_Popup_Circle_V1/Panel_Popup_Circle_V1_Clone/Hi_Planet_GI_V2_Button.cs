using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hi_Planet_GI_V2_Button : GI_V2_Button {
    [HideInInspector]
   public DataItemInput _Data_Item_Input;
    public override void On_Input_Data (string Event_V, string Sub_Event_V, GameObject Script_Go) {
        Event = Event_V; Sub_Event = Sub_Event_V; 
        if (Script_Go != null) {
            _Data_Item_Input = Script_Go.GetComponent <DataItemInput> ();
        }
        // base.On_Input_Data (Event_V, Script_Go);
        On_Setting_Event ();
    }

    public override void Cli_Button () {
      //  Debug.LogError ("Cli_Button");
        if (!b_Select_Img) {
            On_Select_Directly ();
            On_Select_Directly_2 ();
        } else {
            if (!b_On_Select) {
                On_Select_Directly ();
            } else {
                Off_Select_Directly ();
                
            }
        }
    }

    void On_Doing_Event () {
       if (Event == "Inventory" || Event == "Storage" || Event == "Equipment_Setup") {
            if (_Data_Item_Input!=null) {
                HiGameManager.instance._inventoryManager.itemDetail.On_Get_Data (Event, this.gameObject);
            }
       }
    }


    void Off_Doing_Event () {
       
    }

    void On_Setting_Event () {
        if (Sub_Event == "Inventory") { 
            if (_Data_Item_Input != null) {
                A_Image [0].gameObject.SetActive (true);
                if (_Data_Item_Input._Item_Input.Type == "Material") {
                    A_Image[2].gameObject.SetActive (true);
                } else {
                    A_Image[2].gameObject.SetActive (false);
                }
                A_Image [0].sprite = _Data_Item_Input._Item_Input.Item_Sprite;
                A_TMP_Text [0].text = "x " + _Data_Item_Input.Quantity; 
            } else {
                A_Image [0].gameObject.SetActive (false);
                A_Image[2].gameObject.SetActive (false);
            }
          A_Image[1].sprite = HiGameManager.instance._inventoryManager.EmptySlot;
        } else if (Sub_Event == "Equipment_Setup") {
             if (_Data_Item_Input != null && _Data_Item_Input.Id != "") {
                A_Image [0].gameObject.SetActive (true);
                A_Image [0].sprite = _Data_Item_Input._Item_Input.Item_Sprite;
                A_Image[2].gameObject.SetActive (false);
             } else {
                A_Image [0].gameObject.SetActive (false);
             }
        }
        
    }

    void On_Set_Item_Sprite () {
        
    }
    #region using b_Select_Img
    // Line_Tab_Layout :
    public override void Off_Select_Directly () {
        base.Off_Select_Directly ();
    }
    void On_Select_Directly () {
        On_Doing_Event ();
    }
    // this - Business_Location_Canvas :
    public override void On_Select_Directly_2 () {
        base.On_Select_Directly_2 ();
    }
    #endregion
    #region Locked_Img
    
    void On_Locked_Img () {
       // A_Locked_Tx[0].text = Code_Str;
        Locked_Img.gameObject.SetActive (true);
        
    }

    void Off_Locked_Img () {
        Locked_Img.gameObject.SetActive (false);
    }
    #endregion
    #region GI_V2_Button
    public override void On_Send_Same_Data_To_GI_V2_Button (GI_V2_Button Target) {
       
    }
    #endregion
}
